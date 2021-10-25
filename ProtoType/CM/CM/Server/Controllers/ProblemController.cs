using CM.Shared;
using CM.Shared.DataViewModels.BusinessViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CM.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyPolicy")]
    public class ProblemController : ControllerBase
    {
        [Authorize]
        [HttpPost("Add")]
        public async Task<IActionResult> Add(ProblemTypeDataViewModel problemTypeDataView)
        {
            if (problemTypeDataView.Name == "hhe")
            {
                return BadRequest();
            }
            else
            {
                return Ok();
            }

        }

       

      
    }
}
