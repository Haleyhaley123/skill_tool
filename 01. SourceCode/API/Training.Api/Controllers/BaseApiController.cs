using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Training.Api.Controllers
{
    public class BaseApiController : ControllerBase
    {
        protected string GetUserIdByRequest()
        {
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            string signedInUserId = claims.FirstOrDefault(cl => cl.Type.Equals(ClaimTypes.Name))?.Value;

            return signedInUserId;
        }

        protected string GetIdDonViByRequest()
        {
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            string signedInIdDonVi = claims.FirstOrDefault(cl => cl.Type.Equals("IdDonVi"))?.Value;

            return signedInIdDonVi;
        }

        protected string GetLevelDonViByRequest()
        {
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            string signedLevelDonVi = claims.FirstOrDefault(cl => cl.Type.Equals("LevelDonVi"))?.Value;

            return signedLevelDonVi;
        }


        protected bool CheckPermission(string functionCode)
        {
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            var claimPermissions = claims.FirstOrDefault(cl => cl.Type.Equals("Permissions"));
            if (!string.IsNullOrEmpty(claimPermissions?.Value))
            {
                if (!string.IsNullOrEmpty(functionCode))
                {
                    var hasPermission = claimPermissions.Value
                                                        .Split(",")
                                                        .Intersect(new string[] { functionCode });
                    if (hasPermission.Any())
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
