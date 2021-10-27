using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Library.DataModels.BusinessModels
{
    public class CarDataModel
    {
        public CarDataModel()
        {
            this.OwnedCars = new HashSet<OwnedCarDataModel>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string Description { get; set; }

        public virtual CarBrandDataModel CarBrand { get; set; }

        public virtual ICollection<OwnedCarDataModel> OwnedCars { get; set; }

    }
}
