using System;
namespace CM.SharedWithClient
{
	public class MessageContentDataViewModel
	{
        public string? Id { get; set; }

        public string? Text { get; set; }

        PictureBase64DataViewModel? Picture { get; set; }

        public string? InitId { get; set; }
    }
}

