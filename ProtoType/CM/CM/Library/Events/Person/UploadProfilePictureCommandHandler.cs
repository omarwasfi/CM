using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using CM.Library.DataModels;
using CM.Library.DBContexts;
using CM.Library.Queries.Person;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace CM.Library.Events.Person
{
	public class UploadProfilePictureCommandHandler : IRequestHandler<UploadProfilePictureCommand>
	{
        private IHostingEnvironment _hostingEnvironment;
        private CurrentStateDBContext _currentStateDBContext;
        private IMediator _mediator;

        public UploadProfilePictureCommandHandler(IMediator mediator ,IHostingEnvironment hostingEnvironment,CurrentStateDBContext currentStateDBContext)
		{
            this._hostingEnvironment = hostingEnvironment;
            this._mediator = mediator;
            this._currentStateDBContext = currentStateDBContext;
		}

        public async Task<Unit> Handle(UploadProfilePictureCommand request, CancellationToken cancellationToken)
        {
            PersonDataModel person = await _mediator.Send(new GetTheAuthorizedPersonQuery(request.ClaimsPrincipal));

            PictureDataModel pictureDataModel = new PictureDataModel();
            pictureDataModel.Path =  $"/App_Data/Users_Data/{person.Id}/ProfilePicture/";

            createTheFolderOrCheckIfItsExists(_hostingEnvironment.WebRootPath +  pictureDataModel.Path);

            _currentStateDBContext.Pictures.Add(pictureDataModel);

            pictureDataModel.FileName = pictureDataModel.Id+ "-" + request.FileName;
            pictureDataModel.FileExtension = request.FileExtension;

            await saveThePictureToTheLocalStorage(
                request.FormFile,
                _hostingEnvironment.WebRootPath + pictureDataModel.Path + pictureDataModel.FileName + "." + pictureDataModel.FileExtension);


            person.ProfilePicture = pictureDataModel;

            await _currentStateDBContext.SaveChangesAsync();

            return Unit.Value;
        }

        private async Task createTheFolderOrCheckIfItsExists(string folderPath)
        {
            Directory.CreateDirectory(folderPath);
        }

        private async Task saveThePictureToTheLocalStorage(IFormFile formFile,string saveToPath)
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

