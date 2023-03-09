using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NTS.Common;
using NTS.Common.Resource;
using Training.Services.Log;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Training.Models.Models.UserHistory;

namespace Training.Api.Attributes
{
    public class BaseActionFilterAttribute : ActionFilterAttribute
    {
        #region Log

        /// <summary>
        /// Ghi log khi có request
        /// </summary>
        /// <param name="actionContext"></param>
        public void LogRequest(ActionExecutingContext actionContext)
        {
            var activityService = actionContext.HttpContext.RequestServices.GetRequiredService<ILogEventService>();
            var descriptor = actionContext.ActionDescriptor as ControllerActionDescriptor;
            if (!descriptor.ActionName.StartsWith(NTSConstants.NoLogEvent))
            {
                var actionName = ResourceUtil.GetTextResource(descriptor.ActionName) + " " + ResourceUtil.GetTextResource(descriptor.ControllerName);

                if (!string.IsNullOrEmpty(actionName.Trim()))
                {
                    UserHistoryModel activityModel = new UserHistoryModel()
                    {
                        Content = JsonConvert.SerializeObject(actionContext.ActionArguments),
                        Name = actionName.Trim(),
                    };

                    activityService.LogEventAsync(actionContext.HttpContext.Request, activityModel, GetUserIdByRequest(actionContext));
                }
            }
        }

        /// <summary>
        /// Ghi log trả ra response
        /// </summary>
        /// <param name="resultContext"></param>
        public void LogResponse(ResultExecutedContext resultContext)
        {
            var activityService = resultContext.HttpContext.RequestServices.GetRequiredService<ILogEventService>();
            var descriptor = resultContext.ActionDescriptor as ControllerActionDescriptor;
            if (!descriptor.ActionName.StartsWith(NTSConstants.NoLogEvent))
            {
                var actionName = ResourceUtil.GetTextResource(descriptor.ActionName) + " " + ResourceUtil.GetTextResource(descriptor.ControllerName);

                if (!string.IsNullOrEmpty(actionName.Trim()))
                {
                    UserHistoryModel activityModel = new UserHistoryModel()
                    {
                        Content = JsonConvert.SerializeObject(resultContext.Result),
                        Name = actionName.Trim(),
                        Type = NTSConstants.UserHistory_Type_Data
                    };

                    activityService.LogEventAsync(resultContext.HttpContext.Request, activityModel, GetUserIdByRequest(resultContext));
                }
            }
        }
        #endregion

        #region Protected
        protected string GetUserIdByRequest(ResultExecutedContext resultContext)
        {
            var identity = (ClaimsIdentity)resultContext.HttpContext.User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            string signedInUserId = claims.FirstOrDefault(cl => cl.Type.Equals(ClaimTypes.Name))?.Value;

            return signedInUserId;
        }

        protected string GetUserIdByRequest(ActionExecutingContext actionContext)
        {
            var identity = (ClaimsIdentity)actionContext.HttpContext.User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            string signedInUserId = claims.FirstOrDefault(cl => cl.Type.Equals(ClaimTypes.Name))?.Value;

            return signedInUserId;
        }

        protected string GetUserIdByRequest(ExceptionContext actionContext)
        {
            var identity = (ClaimsIdentity)actionContext.HttpContext.User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            string signedInUserId = claims.FirstOrDefault(cl => cl.Type.Equals(ClaimTypes.Name))?.Value;

            return signedInUserId;
        }
        #endregion

        #region Private
        private string GetIPAddress(ActionExecutingContext actionContext)
        {
            var ip = actionContext.HttpContext.Connection.RemoteIpAddress;
            return ip.ToString();
        }

        private string GetIPAddress(ResultExecutedContext resultContext)
        {
            var ip = resultContext.HttpContext.Connection.RemoteIpAddress;
            return ip.ToString();
        }

        private string GetIPAddress(ExceptionContext resultContext)
        {
            var ip = resultContext.HttpContext.Connection.RemoteIpAddress;
            return ip.ToString();
        }
        #endregion
    }
}
