using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Library.DataModels.BusinessModels
{
    public class OfferFixRequestDataModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [Required]
        public virtual ProblemDataModel Problem { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public OfferFixRequestState State { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        public virtual PersonDataModel From { get; set; }

        /// <summary>
        /// From person's notes
        /// </summary>
        [Column(TypeName = "nvarchar(200)")]
        public string Notes { get; set; }

        public virtual PersonDataModel To { get; set; }
    }
}
