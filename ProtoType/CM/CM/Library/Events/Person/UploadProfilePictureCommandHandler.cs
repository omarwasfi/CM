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
            PictureDataModel pictureDataModel = new PictureDataModel();

            pictureDataModel.FileExtension = request.FileExtension;
            pictureDataModel.FileName = request.FileName;
            pictureDataModel.Path = _hostingEnvironment.WebRootPath +
               pictureDataModel.Path +
               pictureDataModel.FileName +
               "." + pictureDataModel.FileExtension;

            await saveThePictureToTheLocalStorage(request.FormFiles,pictureDataModel);

            PersonDataModel person = await _mediator.Send(new GetTheAuthorizedPersonQuery(request.ClaimsPrincipal));

            person.ProfilePicture = pictureDataModel;

            await _currentStateDBContext.SaveChangesAsync();

            return Unit.Value;
        }

        private async Task saveThePictureToTheLocalStorage(IFormFileCollection FormFiles,PictureDataModel pictureDataModel)
        {
            var file = FormFiles[0];

            using (Stream fs = new FileStream(pictureDataModel.Path
                , FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                file.CopyTo(fs);
                fs.Close();

            }
        }
    }
}

