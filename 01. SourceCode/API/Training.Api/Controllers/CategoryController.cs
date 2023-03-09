using Microsoft.AspNetCore.Mvc;
using NTS.Common.Attributes;
using NTS.Common.Files;
using NTS.Common.Models;
using NTS.Common.Resource;
using System.Threading.Tasks;
using Training.Api;
using Training.Api.Attributes;
using Training.Api.Controllers;
using Training.Models.Models.Category;
using Training.Services.Categorys;

namespace PCMT.Api.Controllers
{
    [Route("api/category")]
    [ApiController]
    [ValidateModel]
    [ApiHandleExceptionSystem]
    [Logging]
    [NTSAuthorize]
    public class CategoryController : BaseApiController
    {
        private readonly ICategoryService _categoryService;
        private readonly IUploadFileService _uploadFileService;
        public CategoryController(ICategoryService CategoryService, IUploadFileService uploadFileService)
        {
            this._categoryService = CategoryService;
            this._uploadFileService = uploadFileService;
        }

        /// <summary>
        /// Tìm kiếm danh mục
        /// </summary>
        /// <param name="modelSearch">Dữ liệu tìm kiếm</param>
        /// <returns></returns>
        [HttpPost]
        [Route("search")]
        [ActionName(TextResourceKey.Action_Search)]
        public async Task<ActionResult<ApiResultModel>> SearchCategory()
        {
            ApiResultModel apiResultModel = new ApiResultModel
            {
                StatusCode = ApiResultConstants.StatusCodeSuccess
            };

            apiResultModel.Data = await _categoryService.SearchGroupCategoryAsync();

            return Ok(apiResultModel);
        }

        /// <summary>
        /// Tìm kiếm danh mục
        /// </summary>
        /// <param name="modelSearch">Dữ liệu tìm kiếm</param>
        /// <returns></returns>
        [HttpPost]
        [Route("table/search")]
        [ActionName(TextResourceKey.Action_Search)]
        [AllowPermission(Permissions = "F000550")]
        public async Task<ActionResult<ApiResultModel>> SearchCategoryTable(CategorySearchModel searchModel)
        {
            ApiResultModel apiResultModel = new ApiResultModel
            {
                StatusCode = ApiResultConstants.StatusCodeSuccess
            };

            apiResultModel.Data = await _categoryService.SearchCategoryTableAsync(searchModel);

            return Ok(apiResultModel);
        }

        /// <summary>
        /// Thêm mới danh mục
        /// </summary>
        /// <param name="model">Dữ liệu thêm mới</param>
        /// <returns></returns>
        [HttpPost]
        [ActionName(TextResourceKey.Action_Create)]
        [AllowPermission(Permissions = "F000551")]
        public async Task<ActionResult<ApiResultModel>> CreateCategory([FromBody] CategoryCreateModel model)
        {
            ApiResultModel apiResultModel = new ApiResultModel
            {
                StatusCode = ApiResultConstants.StatusCodeSuccess
            };

            await _categoryService.CreateCategoryAsync(model);

            return Ok(apiResultModel);
        }

        /// <summary>
        /// Cập nhật danh mục
        /// </summary>
        /// <param name="id">Id danh mục</param>
        /// <param name="model">Dữ liệu cập nhật</param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        [ActionName(TextResourceKey.Action_Update)]
        [AllowPermission(Permissions = "F000552")]
        public async Task<ActionResult<ApiResultModel>> UpdateCategory([FromRoute] string id, [FromBody] CategoryCreateModel model)
        {
            ApiResultModel apiResultModel = new ApiResultModel
            {
                StatusCode = ApiResultConstants.StatusCodeSuccess
            };

            await _categoryService.UpdateCategoryAsync(id, model);

            return Ok(apiResultModel);
        }

        /// <summary>
        /// Xóa danh mục
        /// </summary>
        /// <param name="id">Id danh mục</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        [ActionName(TextResourceKey.Action_Delete)]
        [AllowPermission(Permissions = "F000553")]
        public async Task<ActionResult<ApiResultModel>> DeleteCategory([FromRoute] string id)
        {
            ApiResultModel apiResultModel = new ApiResultModel
            {
                StatusCode = ApiResultConstants.StatusCodeSuccess
            };

            await _categoryService.DeleteCategoryByIdAsync(id);

            return Ok(apiResultModel);
        }

        /// <summary>
        /// Lấy thông tin danh mục theo id
        /// </summary>
        /// <param name="id">Id danh mục</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [ActionName(TextResourceKey.Action_Get)]
        [AllowPermission(Permissions = "F000552")]
        public async Task<ActionResult<ActionResult<CategoryCreateModel>>> GetCategoryById([FromRoute] string id)
        {
            ApiResultModel apiResultModel = new ApiResultModel
            {
                StatusCode = ApiResultConstants.StatusCodeSuccess
            };

            apiResultModel.Data = await _categoryService.GetCategoryByIdAsync(id);

            return Ok(apiResultModel);
        }

        /// <summary>
        /// Lấy danh sách thứ tự hiển thị
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-list-oder")]
        [AllowPermission(Permissions = "F000551;F000552")]
        public async Task<ActionResult<ApiResultModel>> GetListOrder(string id)
        {
            ApiResultModel apiResultModel = new ApiResultModel
            {
                StatusCode = ApiResultConstants.StatusCodeSuccess
            };

            apiResultModel.Data = await _categoryService.GetListOrderAsync(id);

            return Ok(apiResultModel);
        }

        /// <summary>
        /// Thêm mới danh mục theo tên bảng
        /// </summary>
        /// <param name="model">Dữ liệu thêm mới</param>
        /// <returns></returns>
        [HttpPost]
        [Route("table")]
        [ActionName(TextResourceKey.Action_Create)]
        [AllowPermission(Permissions = "F000551")]
        public async Task<ActionResult<ApiResultModel>> CreateCategoryTableAsync([FromBody] CategoryModel model)
        {
            ApiResultModel apiResultModel = new ApiResultModel
            {
                StatusCode = ApiResultConstants.StatusCodeSuccess
            };

            await _categoryService.CreateCategoryTableAsync(model);

            return Ok(apiResultModel);
        }

        /// <summary>
        /// Cập nhật danh mục theo tên bảng
        /// </summary>
        /// <param name="id">Id danh mục</param>
        /// <param name="model">Dữ liệu cập nhật</param>
        /// <returns></returns>
        [HttpPut]
        [Route("table/{id}")]
        [ActionName(TextResourceKey.Action_Update)]
        [AllowPermission(Permissions = "F000552")]
        public async Task<ActionResult<ApiResultModel>> UpdateCategoryTable([FromRoute] string id, [FromBody] CategoryModel model)
        {
            ApiResultModel apiResultModel = new ApiResultModel
            {
                StatusCode = ApiResultConstants.StatusCodeSuccess
            };

            await _categoryService.UpdateCategoryTableAsync(id, model);

            return Ok(apiResultModel);
        }

        /// <summary>
        /// Xóa danh mục theo tên bảng
        /// </summary>
        /// <param name="id">Id danh mục</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("table/{id}")]
        [ActionName(TextResourceKey.Action_Delete)]
        [AllowPermission(Permissions = "F000553")]
        public async Task<ActionResult<ApiResultModel>> DeleteCategoryTableAsync([FromRoute] string id, string tableName)
        {
            ApiResultModel apiResultModel = new ApiResultModel
            {
                StatusCode = ApiResultConstants.StatusCodeSuccess
            };

            await _categoryService.DeleteCategoryTableAsync(id, tableName);

            return Ok(apiResultModel);
        }

        /// <summary>
        /// Lấy thông tin danh mục theo id
        /// </summary>
        /// <param name="id">Id danh mục</param>
        /// <returns></returns>
        [HttpGet]
        [Route("table/{id}")]
        [ActionName(TextResourceKey.Action_Get)]
        [AllowPermission(Permissions = "F000552")]
        public async Task<ActionResult<ActionResult<CategoryCreateModel>>> GetCategoryTableById([FromRoute] string id, string tableName)
        {
            ApiResultModel apiResultModel = new ApiResultModel
            {
                StatusCode = ApiResultConstants.StatusCodeSuccess
            };

            apiResultModel.Data = await _categoryService.GetCategoryTableByIdAsync(id, tableName);

            return Ok(apiResultModel);
        }

        /// <summary>
        /// Lấy danh sách thứ tự hiển thị
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("table/get-list-oder")]
        [AllowPermission(Permissions = "F000551;F000552")]
        public async Task<ActionResult<ApiResultModel>> GetListOrderTable(string id, string tableName)
        {
            ApiResultModel apiResultModel = new ApiResultModel
            {
                StatusCode = ApiResultConstants.StatusCodeSuccess
            };

            apiResultModel.Data = await _categoryService.GetListOrderTableAsync(id, tableName);

            return Ok(apiResultModel);
        }
    }
}
