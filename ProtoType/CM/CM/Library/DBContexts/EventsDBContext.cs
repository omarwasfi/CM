using CM.Library.DataModels;
using CM.Library.DataModels.Events;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Library.DBContexts
{
    [System.Data.Entity.DbConfigurationType(typeof(MySql.Data.EntityFramework.MySqlEFConfiguration))]
    public class EventsDBContext : DbContext 
    {

        public DbSet<EventDataModel> Events { get; set; }


        public EventsDBContext(DbContextOptions<EventsDBContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

       
    }
}
