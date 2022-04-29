using EFWebApi_v2.Configuration;
using EFWebApi_v2.Data;
using EFWebApi_v2.Models;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EFWebapi_v2.Logging
{
    public class DbLogger : ILogger
    {
        private readonly string _loggerName;
        private readonly DbLoggerConfiguration _config;
        private readonly SqlEfContex _sqlEfContex;

        public DbLogger(string loggerName, DbLoggerConfiguration config)
        {
            _loggerName = loggerName;

            _config = config;
        }

        public bool IsEnabled(LogLevel logLevel) => true;


        public IDisposable BeginScope<TState>(TState state) => default;

        

        public void Log<TState>(LogLevel logLevel,EventId eventId,TState state,Exception exception,Func<TState, Exception, string> formatter)
        {
            
            //Write to file Logsoutput.txt
            string fileName = Path.Combine(Environment.CurrentDirectory, "Logsoutput.txt");

            //Create New File
            StreamWriter stream = new StreamWriter(fileName);
            stream.WriteLine("Start Log");
            stream.Close();

            //Update exist file
            StreamWriter st = new StreamWriter(fileName, true);
            st.WriteLine("Append log" + formatter(state, exception));
            st.Close();
        
            //Console.WriteLine(logLevel.ToString(),  state, exception, formatter);
           // RecordMsg(logLevel, eventId, state, exception, formatter);
        }

       /* public void RecordMsg<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {            
                _sqlEfContex.Logs.Add(new LogM
                    {
                        EventId = eventId.Id,
                        LogLevel = logLevel.ToString(),
                        CategoryName = _loggerName,
                        Message = formatter(state, exception),
                        Created = DateTime.Now.ToString(),
                    });
            
                _sqlEfContex.SaveChanges();
        }
       */
    }

            

}

