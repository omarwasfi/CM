using CM.Shared.DataViewModels.BusinessViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Shared.DataViewModels
{
    public class PersonDataViewModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        [MaxLength(100, ErrorMessage = "FirstName Can't be more than 100 characters")]
        public string FirstName { get; set; }
       
        [MaxLength(100, ErrorMessage = "LastName Can't be more than 100 characters")]
        public string LastName { get; set; }

        [Required]
        public bool IsAFixer { get; set; }

        

        


    }
}
