namespace CareerCloud.WebAPI.Controllers
{
    using CareerCloud.BusinessLogicLayer;
    using CareerCloud.EntityFrameworkDataAccess;
    using CareerCloud.Pocos;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [Route("api/careercloud/System/v1")]
    [ApiController]
    public class SystemCountryCodeController : ControllerBase
    {
        SystemCountryCodeLogic _countryCodeLogic;

        public SystemCountryCodeController()
        {
            var repository = new EFGenericRepository<SystemCountryCodePoco>();
            _countryCodeLogic = new SystemCountryCodeLogic(repository);
        }

        [HttpGet]
        [Route("countryCode")]
        public ActionResult GetSystemCountryCode()
        {
            return new OkObjectResult(_countryCodeLogic.GetAll());
        }

        [HttpGet]
        [Route("countryCode/{systemCountryCodeId}")]
        public ActionResult GetSystemCountryCode(string systemCountryCodeId)
        {
            SystemCountryCodePoco item = _countryCodeLogic.Get(systemCountryCodeId);
            if (item == null)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(item);
        }

        [HttpPost]
        [Route("countryCode")]
        public ActionResult PostSystemCountryCode([FromBody] SystemCountryCodePoco[] items)
        {
            try
            {
                _countryCodeLogic.Add(items);
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
        [Route("countryCode")]
        public ActionResult PutSystemCountryCode([FromBody] SystemCountryCodePoco[] items)
        {
            try
            {
                _countryCodeLogic.Update(items);
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
        [Route("countryCode")]
        public ActionResult DeleteSystemCountryCode([FromBody] SystemCountryCodePoco[] items)
        {
            _countryCodeLogic.Delete(items);
            return new OkResult();
        }
    }
}
