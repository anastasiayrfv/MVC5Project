using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Data.Entity;

namespace TestTask.Models
{
    public class Tender
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Desciption { get; set; }
        public string Organizer { get; set; }
        public string Kind { get; set; }
        public string Category { get; set; }
        public double Price { get; set; }
        public string Currency { get; set; }
        public DateTime Date { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

      
    }

    
}