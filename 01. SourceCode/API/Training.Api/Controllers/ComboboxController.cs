using Microsoft.AspNetCore.Mvc;
using NTS.Common;
using NTS.Common.Models;
using System;
using System.Threading.Tasks;
using Training.Api.Controllers;
using Training.Models.Models.Combobox;
using Training.Services.Combobox;

namespace PCMT.Api.Controllers
{
    [Route("api/combobox")]
    [ApiController]
    public class ComboboxController : BaseApiController
    {
        private readonly IComboboxService comboboxService;

        public ComboboxController(IComboboxService comboboxService)
        {
            this.comboboxService = comboboxService;
        }

        [HttpPost]
        [Route("get-list-tinh")]
        public async Task<ActionResult<ApiResultModel>> GetAllTinh([FromBody] ComboboxSearchModel searchModel)
        {
            ApiResultModel apiResultModel = new ApiResultModel
            {
                StatusCode = ApiResultConstants.StatusCodeSuccess
            };

            //apiResultModel.Data = await comboboxService.GetAllTinh(searchModel.IsBienGioi);

            return Ok(apiResultModel);
        }

        
    }
}
