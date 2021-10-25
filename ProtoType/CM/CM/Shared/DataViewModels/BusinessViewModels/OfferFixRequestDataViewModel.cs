using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Shared.DataViewModels.BusinessViewModels
{
    public class OfferFixRequestDataViewModel
    {
        public string Id { get; }

        [Required]
        public ProblemDataViewModel Problem { get; set; }

        public string State { get; }

        public DateTime DateTime { get; }

        public PersonDataViewModel From { get; set; }

        /// <summary>
        /// From person's notes
        /// </summary>
        [MaxLength(200, ErrorMessage = "Notes Can't be more than 200 characters")]
        public string Notes { get; set; }

        public PersonDataViewModel To { get; set; }
    }
}
