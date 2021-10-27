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

        #endregion

        #region CarBrand
        AddCarBrand,
        #endregion

        #region ProblemType

        AddNewProblemType,
        AddNewProblem

        #endregion

    }
}