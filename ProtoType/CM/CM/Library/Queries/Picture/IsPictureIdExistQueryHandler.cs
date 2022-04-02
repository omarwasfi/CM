using System;
using System.Threading;
using System.Threading.Tasks;
using CM.Library.DataModels;
using CM.Library.DBContexts;
using MediatR;

namespace CM.Library.Queries.Picture
{
	public class IsPictureIdExistQueryHandler : IRequestHandler<IsPictureIdExistQuery,bool>
	{
        private readonly CurrentStateDBContext _currentStateDBContext;

        public IsPictureIdExistQueryHandler(CurrentStateDBContext currentStateDBContext)
		{
            this._currentStateDBContext = currentStateDBContext;
		}

        public async Task<bool> Handle(IsPictureIdExistQuery request, CancellationToken cancellationToken)
        {
            PictureDataModel picture =  await _currentStateDBContext.Pictures.FindAsync(request.Id);
            if(picture != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

