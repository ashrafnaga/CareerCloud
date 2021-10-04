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
    public class SecurityLoginsLogController : ControllerBase
    {
        SecurityLoginsLogLogic _loginsLogLogic;

        public SecurityLoginsLogController()
        {
            var repository = new EFGenericRepository<SecurityLoginsLogPoco>();
            _loginsLogLogic = new SecurityLoginsLogLogic(repository);
        }

        [HttpGet]
        [Route("loginsLog")]
        public ActionResult GetSecurityLoginLog()
        {
            return new OkObjectResult(_loginsLogLogic.GetAll());
        }

        [HttpGet]
        [Route("loginsLog/{securityLoginsLogId}")]
        public ActionResult GetSecurityLoginLog(Guid securityLoginsLogId)
        {
            SecurityLoginsLogPoco item = _loginsLogLogic.Get(securityLoginsLogId);
            if (item == null)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(item);
        }

        [HttpPost]
        [Route("loginsLog")]
        public ActionResult PostSecurityLoginLog([FromBody] SecurityLoginsLogPoco[] items)
        {
            try
            {
                _loginsLogLogic.Add(items);
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
        [Route("loginsLog")]
        public ActionResult PutSecurityLoginLog([FromBody] SecurityLoginsLogPoco[] items)
        {
            try
            {
                _loginsLogLogic.Update(items);
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
        [Route("loginsLog")]
        public ActionResult DeleteSecurityLoginLog([FromBody] SecurityLoginsLogPoco[] items)
        {
            _loginsLogLogic.Delete(items);
            return new OkResult();
        }
    }
}
