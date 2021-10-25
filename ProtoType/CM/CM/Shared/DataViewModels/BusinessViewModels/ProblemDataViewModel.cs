﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Shared.DataViewModels.BusinessViewModels
{
    public class ProblemDataViewModel
    {
        public string Id { get;}

        [Required]
        public ProblemTypeDataViewModel ProblemType { get; set; }

        [Required]
        [MaxLength(400, ErrorMessage = "Description Can't be more than 400 characters")]
        public string Description { get; set; }

        public DateTime DateTime { get; set; }

        public string Location { get; set; }
    }
}
