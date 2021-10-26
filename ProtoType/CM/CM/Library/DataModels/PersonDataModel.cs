using CM.Library.DataModels.BusinessModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Library.DataModels
{
    public class PersonDataModel : IdentityUser
    {
        [Column(TypeName = "nvarchar(100)")]
        public string FirstName { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string LastName { get; set; }
        public bool IsAFixer { get; set; } = false;
        public virtual ICollection<OwendCarDataModel> OwendCars { get; set; }
        public virtual ICollection<ProblemDataModel> Problems { get; set; }

    }
}
