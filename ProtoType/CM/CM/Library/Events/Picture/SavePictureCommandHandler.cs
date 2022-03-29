using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using CM.Library.DataModels;
using CM.Library.DBContexts;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace CM.Library.Events.Picture
{
	public class SavePictureCommandHandler : IRequestHandler<SavePictureCommand,PictureDataModel>
	{
        private IHostingEnvironment _hostingEnvironment;
        private CurrentStateDBContext _currentStateDBContext;

        public SavePictureCommandHandler( IHostingEnvironment hostingEnvironment, CurrentStateDBContext currentStateDBContext)
        {
            this._hostingEnvironment = hostingEnvironment;
            this._currentStateDBContext = currentStateDBContext;
        }

        public async Task<PictureDataModel> Handle(SavePictureCommand request, CancellationToken cancellationToken)
        {
            PictureDataModel pictureDataModel = new PictureDataModel();
            pictureDataModel.Path = $"/App_Data/Users_Data/{request.PersonId}/{request.SubFolderName}/";

            createTheFolderOrCheckIfItsExists(_hostingEnvironment.WebRootPath + pictureDataModel.Path);

            _currentStateDBContext.Pictures.Add(pictureDataModel);


            pictureDataModel.FileName = pictureDataModel.Id + "-" + request.FormFile.FileName.Substring(0, request.FormFile.FileName.IndexOf(".")); ;
            pictureDataModel.FileExtension = request.FormFile.FileName.Substring(request.FormFile.FileName.IndexOf(".") + 1);

            await saveThePictureToTheLocalStorage(
                request.FormFile,
                _hostingEnvironment.WebRootPath + pictureDataModel.Path + pictureDataModel.FileName + "." + pictureDataModel.FileExtension);

            await _currentStateDBContext.SaveChangesAsync();

            return pictureDataModel;
        }

        private async Task createTheFolderOrCheckIfItsExists(string folderPath)
        {
            Directory.CreateDirectory(folderPath);
        }

        private async Task saveThePictureToTheLocalStorage(IFormFile formFile, string saveToPath)
        {

            IFormFile file = formFile;

            using (Stream fs = new FileStream(saveToPath
                , FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                file.CopyTo(fs);
                fs.Close();

            }
        }
    }
}

