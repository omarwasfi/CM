using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Library.DataModels.BusinessModels
{
    public class FixRequestDataModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        
        [Required]
        public ProblemDataModel Problem { get; set; }
        public DateTime DateTime{ get; set; }

        public PersonDataModel From { get; set; }

        public PersonDataModel To { get; set; }
    }
}
