using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Shared.DataViewModels.BusinessViewModels
{
    public class CarDataViewModel
    {
        public string Id { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Car Name Can't be more than 100 characters")]
        public string Name { get; set; }

        [MaxLength(100, ErrorMessage = "Car descrition Can't be more than 200 characters")]

        public string Description { get; set; }

        public CarBrandDataViewModel CarBrand { get; set; }


    }
}
