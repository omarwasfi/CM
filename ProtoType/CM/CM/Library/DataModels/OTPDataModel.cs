using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CM.Library.DataModels
{
	public class OTPDataModel
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public string Value { get; set; }

        public DateTime ExpirationDate { get; set; }

        public virtual PersonDataModel Person { get; set; }
    }
}

