using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Shared.DataViewModels.BusinessViewModels
{
    public class OwendCarDataViewModel
    {
        public string Id { get;  }

        [MaxLength(100, ErrorMessage = "Name Can't be more than 100 characters")]
        public string Name { get; set; }

        [MaxLength(200, ErrorMessage = "Description Can't be more than 200 characters")]
        public string Description { get; set; }
    }
}
