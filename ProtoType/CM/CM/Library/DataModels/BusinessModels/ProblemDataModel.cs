using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Library.DataModels.BusinessModels
{
    public class ProblemDataModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [Required]
        public virtual ProblemTypeDataModel ProblemType { get; set; }
       
        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public ProblemState State { get; set; }

        [Required]
        public virtual PersonDataModel Person { get; set; }

        [Required]
        public virtual OwnedCarDataModel OwnedCar { get; set; }



        [Required]
        [Column(TypeName = "nvarchar(400)")]
        public string Description { get; set; }

        public DateTime DateTime { get; set; }

        public string Location { get; set; }

    }
}
