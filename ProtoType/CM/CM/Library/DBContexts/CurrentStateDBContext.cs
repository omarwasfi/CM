using CM.Library.DataModels;
using CM.Library.DataModels.BusinessModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Library.DBContexts
{
    [System.Data.Entity.DbConfigurationType(typeof(MySql.Data.EntityFramework.MySqlEFConfiguration))]
    public class CurrentStateDBContext : IdentityDbContext<PersonDataModel>
    {
        public DbSet<CarBrandDataModel> CarBrands { get; set; }
        public DbSet<CarDataModel> Cars { get; set; }
        public DbSet<FixRequestDataModel> FixRequests { get; set; }
        public DbSet<OfferFixRequestDataModel> OfferFixRequests { get; set; }
        public DbSet<OwnedCarDataModel> OwnedCars { get; set; }
        public DbSet<ProblemDataModel> Problems { get; set; }
        public DbSet<ProblemTypeDataModel> ProblemTypes { get; set; }


        public CurrentStateDBContext(DbContextOptions<CurrentStateDBContext> options) : base(options)
        {


        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies(true);
        }
       

    }
}
