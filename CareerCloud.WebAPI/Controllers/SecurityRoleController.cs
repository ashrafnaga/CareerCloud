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
    public class SecurityRoleController : ControllerBase
    {
        SecurityRoleLogic _roleLogic;

        public SecurityRoleController()
        {
            var repository = new EFGenericRepository<SecurityRolePoco>();
            _roleLogic = new SecurityRoleLogic(repository);
        }

        [HttpGet]
        [Route("role")]
        public ActionResult GetSecurityRole()
        {
            return new OkObjectResult(_roleLogic.GetAll());
        }

        [HttpGet]
        [Route("role/{securityRoleId}")]
        public ActionResult GetSecurityRole(Guid securityRoleId)
        {
            SecurityRolePoco item = _roleLogic.Get(securityRoleId);
            if (item == null)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(item);
        }

        [HttpPost]
        [Route("role")]
        public ActionResult PostSecurityRole([FromBody] SecurityRolePoco[] items)
        {
            try
            {
                _roleLogic.Add(items);
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
        [Route("role")]
        public ActionResult PutSecurityRole([FromBody] SecurityRolePoco[] items)
        {
            try
            {
                _roleLogic.Update(items);
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
        [Route("role")]
        public ActionResult DeleteSecurityRole([FromBody] SecurityRolePoco[] items)
        {
            _roleLogic.Delete(items);
            return new OkResult();
        }
    }
}
