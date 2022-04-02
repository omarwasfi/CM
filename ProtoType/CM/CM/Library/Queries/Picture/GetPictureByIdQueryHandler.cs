using System;
using System.Threading;
using System.Threading.Tasks;
using CM.Library.DataModels;
using CM.Library.DBContexts;
using MediatR;

namespace CM.Library.Queries.Picture
{
	public class GetPictureByIdQueryHandler : IRequestHandler<GetPictureByIdQuery,PictureDataModel>
	{
        private readonly CurrentStateDBContext _currentStateDBContext;

        public GetPictureByIdQueryHandler(CurrentStateDBContext currentStateDBContext)
		{
            this._currentStateDBContext = currentStateDBContext;
		}

        public async Task<PictureDataModel> Handle(GetPictureByIdQuery request, CancellationToken cancellationToken)
        {
            return await _currentStateDBContext.Pictures.FindAsync(request.Id);
        }
    }
}

