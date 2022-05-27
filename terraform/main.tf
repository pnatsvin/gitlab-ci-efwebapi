terraform {
  required_providers {
    yandex = {
      source = "yandex-cloud/yandex"
    }
  }
  required_version = ">= 0.13"
}

provider "yandex" {
  token     = "AQAAAABXnlfCAATuwfZehxfiAE_utricbBXGZu0"
  cloud_id  = "b1gaq7sfo7btg6qur1d1"
  folder_id = "b1glq7cpa2cm551ofaa5"
  zone      = "ru-central1-a"
}

resource "yandex_compute_instance" "firstvm" {
  name        = "sqlexpress-terraform"
  platform_id = "standard-v3"
  zone        = "ru-central1-a"

  resources {
    cores  = 2
    memory = 2
    core_fraction = 50
    
  }

  boot_disk {
    initialize_params {
      image_id = "fd8ciuqfa001h8s9sa7i"
      size = 15
      type = "network-hdd"
    }
  }

  network_interface {
    subnet_id = "e9b2d3chqi1kl5qe4an2"
    nat = "true"  
}

scheduling_policy {
    preemptible = "true"
}

metadata = {
    user-data = "${file("/home/pnats/terraform/meta.txt")}"
}
}

resource "yandex_compute_instance" "vm-2" {
  name        = "net-runner-terraform"
  platform_id = "standard-v3"
  zone        = "ru-central1-a"

  resources {
    cores  = 2
    memory = 4
    core_fraction = 100

  }

  boot_disk {
    initialize_params {
      image_id = "fd8ciuqfa001h8s9sa7i"
      size = 20
      type = "network-hdd"
    }
  }

  network_interface {
    subnet_id = "e9b2d3chqi1kl5qe4an2"
    nat = "true"
}

scheduling_policy {
    preemptible = "true"
}

metadata = {
    user-data = "${file("/home/pnats/terraform/meta.txt")}"
}
}
