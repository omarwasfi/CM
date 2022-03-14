using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Library.DataModels.Events
{
    public enum EventType
    {
        #region Person
        
        LoginPerson,
        RegisterPerson,
        LogoutPerson,
        GenerateToken,
        UpdateProfile,

        #endregion

        #region Room
        CreateRoom,

        #endregion

        #region CarBrand
        AddCarBrand,
        #endregion

        #region Car
        AddCar,
        AddOwnedCar,
        #endregion

        #region ProblemType

        AddNewProblemType,
        AddNewProblem

        #endregion

    }
}