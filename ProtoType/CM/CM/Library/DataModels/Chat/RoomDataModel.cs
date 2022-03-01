using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CM.Library.DataModels.Chat
{
	public class RoomDataModel
	{
        public RoomDataModel()
        {
            this.Messages = new HashSet<MessageDataModel>();
            this.People = new HashSet<PersonDataModel>();
        }

        [Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public string Id { get; set; }

        public virtual ICollection<MessageDataModel> Messages { get; set; }

        public virtual ICollection<PersonDataModel> People { get; set; }

    }
}

