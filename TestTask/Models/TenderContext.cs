using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace TestTask.Models
{
    public class TenderContext : DbContext
    {
        public TenderContext()
         : base("DbConnection")
        { }
        public DbSet<Tender> Tenders { get; set; }

    }
}