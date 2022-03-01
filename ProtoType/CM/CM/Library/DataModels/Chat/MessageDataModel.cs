using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CM.Library.DataModels.Chat
{
	public class MessageDataModel
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public string Id { get; set; }

		public DateTime DateTime { get; set; }

		public virtual PersonDataModel From { get; set; }

        public virtual RoomDataModel Room { get; set; }

        public virtual MessageContentDataModel MessageContent { get; set; }

    }
}

