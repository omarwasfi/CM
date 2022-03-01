using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CM.Library.DataModels.Chat
{
	public class MessageContentDataModel
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public string Id { get; set; }

		[Required]
		[Column(TypeName = "nvarchar(100)")]
		public MessageContentType Type { get; set; }

        public string Text { get; set; }

        public virtual PictureDataModel Picture { get; set; }


    }
}

