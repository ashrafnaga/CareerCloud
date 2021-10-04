namespace CareerCloud.WebAPI.Controllers
{
    using CareerCloud.BusinessLogicLayer;
    using CareerCloud.EntityFrameworkDataAccess;
    using CareerCloud.Pocos;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [Route("api/careercloud/security/v1")]
    [ApiController]
    public class SecurityLoginsRoleController : ControllerBase
    {
        SecurityLoginsRoleLogic _loginsRoleLogic;

        public SecurityLoginsRoleController()
        {
            var repository = new EFGenericRepository<SecurityLoginsRolePoco>();
            _loginsRoleLogic = new SecurityLoginsRoleLogic(repository);
        }

        [HttpGet]
        [Route("loginsRole")]
        public ActionResult GetSecurityLoginsRole()
        {
            return new OkObjectResult(_loginsRoleLogic.GetAll());
        }

        [HttpGet]
        [Route("loginsRole/{securityLoginsRoleId}")]
        public ActionResult GetSecurityLoginsRole(Guid securityLoginsRoleId)
        {
            SecurityLoginsRolePoco item = _loginsRoleLogic.Get(securityLoginsRoleId);
            if (item == null)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(item);
        }

        [HttpPost]
        [Route("loginsRole")]
        public ActionResult PostSecurityLoginRole([FromBody] SecurityLoginsRolePoco[] items)
        {
            try
            {
                _loginsRoleLogic.Add(items);
                return new OkResult();
            }
            catch (AggregateException ex)
            {
                Dictionary<int, string> validationErrors = new Dictionary<int, string>();
                foreach (var exeption in ex.InnerExceptions)
                {
                    int code = ((ValidationException)exeption).Code;
                    validationErrors.Add(code, exeption.Message);
                }
                return BadRequest(validationErrors.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("loginsRole")]
        public ActionResult PutSecurityLoginsRole([FromBody] SecurityLoginsRolePoco[] items)
        {
            try
            {
                _loginsRoleLogic.Update(items);
                return new OkResult();
            }
            catch (AggregateException ex)
            {
                Dictionary<int, string> validationErrors = new Dictionary<int, string>();
                foreach (var exeption in ex.InnerExceptions)
                {
                    int code = ((ValidationException)exeption).Code;
                    validationErrors.Add(code, exeption.Message);
                }
                return BadRequest(validationErrors.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("loginsRole")]
        public ActionResult DeleteSecurityLoginRole([FromBody] SecurityLoginsRolePoco[] items)
        {
            _loginsRoleLogic.Delete(items);
            return new OkResult();
        }
    }
}
