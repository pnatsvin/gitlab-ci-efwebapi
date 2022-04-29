using Microsoft.EntityFrameworkCore;
using EFWebApi_v2.Models;

namespace EFWebApi_v2.Data
{
    public class SqlEfContex : DbContext
    {
        public SqlEfContex(DbContextOptions dbContextOptions) : base(dbContextOptions) { }

        public DbSet<Record> Days { get; set; }

    }
}
