using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using CM.Library.DataModels;
using CM.Library.DBContexts;
using CM.Library.Events.Picture;
using CM.Library.Queries.Person;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace CM.Library.Events.Person
{
	public class UploadProfilePictureCommandHandler : IRequestHandler<UploadProfilePictureCommand>
	{
        private CurrentStateDBContext _currentStateDBContext;
        private IMediator _mediator;

        public UploadProfilePictureCommandHandler(IMediator mediator ,CurrentStateDBContext currentStateDBContext)
		{
            this._mediator = mediator;
            this._currentStateDBContext = currentStateDBContext;
		}

        public async Task<Unit> Handle(UploadProfilePictureCommand request, CancellationToken cancellationToken)
        {
            PersonDataModel person = await _mediator.Send(new GetTheAuthorizedPersonQuery(request.ClaimsPrincipal));

            

            PictureDataModel pictureDataModel = await _mediator.Send(
                new SavePictureCommand
                (
                    formFile: request.FormFile,
                    personId: person.Id,
                    subFolderName: "ProfilePicture"
                    )
                );


            person.ProfilePicture = pictureDataModel;

            await _currentStateDBContext.SaveChangesAsync();

            return Unit.Value;
        }

        
    }
}

