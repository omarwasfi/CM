using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Library.DataModels.BusinessModels
{
    public class OwnedCarDataModel
    {
        public OwnedCarDataModel()
        {
            this.Problems = new HashSet<ProblemDataModel>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string Description { get; set; }

        public virtual CarDataModel Car { get; set; }

        public virtual PersonDataModel Person { get; set; }

        public virtual ICollection<ProblemDataModel> Problems { get; set; }
    }
}
