using EFWebapi_v2.Logging;
using EFWebApi_v2.Data;
using EFWebApi_v2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EFWebapi_v2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecordController : ControllerBase
    {
        private readonly SqlEfContex _context;

        private readonly ILogger<DbLogger> _logger;



        public RecordController(SqlEfContex contex, ILogger<DbLogger> logger)
        {
            _context = contex;
            _logger = logger;
        }


        [HttpGet(Name = "GetAllRecords")] // Посмотреть все записи
        public IEnumerable<Record> Get()
        {
            
            return _context.Days;
        }
      
        [HttpGet("Create")] //Выполнить функцию и создать запись в базе
        public string Get(int Year, int Month, int Day)
        {
            Record record = new();
            record.Params = "Year " + Year.ToString() + " Month " + Month.ToString() + " Day " + Day.ToString();

            DateTime dt = new(Year, Month, Day);
            var rs = dt.ToString("dddd");


            if (dt.Year == Year && dt.Month == Month && dt.Day == Day)
            {
                record.Result = rs;
                record.Created = DateTime.Now;
            }
            else
            {
                record.Result = "Look carefully to int stings";

            }

          
            _context.Days.Add(record);
            _context.SaveChanges();
            
            return rs;

        }

        [HttpPost("Add_Record")]
        public ActionResult<Record> PostDayOf(Record df)
        {

            if (df == null)
                return BadRequest();

            
            _context.Add(df);
            _context.SaveChanges();
            return CreatedAtAction(nameof(PostDayOf), new Record { Id = df.Id }, df);
          
        }

        [HttpPut("{id}")]
        public ActionResult<Record> PutDayOf(int id, Record df)
        {
            if (id != df.Id)
                return BadRequest();

           
            _context.Entry(df).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<Record> Delete(int id)
        {
            var wr = _context.Days.Find(id);

            if (wr is null)
            {
                return NotFound();
            }

          
            _context.Days.Remove(wr);
            _context.SaveChanges();
            return wr;
        }
    }
}

