using CM.Library.DataModels.BusinessModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Library.DataModels
{
    public class PersonDataModel : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual ICollection<OwendCarDataModel> OwendCars { get; set; }


    }
}
