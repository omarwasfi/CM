using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Hosting;

namespace CM.Library.Queries.Picture
{
	public class GetPictureAsBase64QueryHandler : IRequestHandler<GetPictureAsBase64Query, string>
	{
        private IHostingEnvironment _hostingEnvironment;

        public GetPictureAsBase64QueryHandler(IHostingEnvironment hostingEnvironment)
		{
            this._hostingEnvironment = hostingEnvironment;
		}

        public async Task<string> Handle( GetPictureAsBase64Query request, CancellationToken cancellationToken)
        {
            try
            {
                
                byte[] fileBytes = await convertLocalFileToArrOfByte(
               _hostingEnvironment.WebRootPath +
               request.Picture.Path +
               request.Picture.FileName +
               "." + request.Picture.FileExtension
              );

                return "data:image/"
                        + $"{request.Picture.FileExtension};"
                        + "base64, "
                        + Convert.ToBase64String(fileBytes);
            }
            catch
            {
                byte[] fileBytes = await convertLocalFileToArrOfByte(
              _hostingEnvironment.WebRootPath +
              request.DefaultProfilePictureFilePath +
              request.DefaultProfilePictureFileName +
              "." + request.DefaultProfilePictureFileExtension
             );

                return "data:image/"
                       + $"{"jpg"};"
                       + "base64, "
                       + Convert.ToBase64String(fileBytes);
            }
          
        }

        private async Task<byte[]> convertLocalFileToArrOfByte(string localPath)
        {
            using (FileStream fs = new FileStream(localPath, FileMode.Open, FileAccess.Read))
            {
                byte[] bytes = System.IO.File.ReadAllBytes(localPath);
                fs.Read(bytes, 0, System.Convert.ToInt32(fs.Length));
                fs.Close();
                return bytes;

            }
        }
    }
}

