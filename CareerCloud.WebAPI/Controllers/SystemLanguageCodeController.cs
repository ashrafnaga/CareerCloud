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
    public class SystemLanguageCodeController : ControllerBase
    {
        SystemLanguageCodeLogic _languageCodeLogic;

        public SystemLanguageCodeController()
        {
            var repository = new EFGenericRepository<SystemLanguageCodePoco>();
            _languageCodeLogic = new SystemLanguageCodeLogic(repository);
        }

        [HttpGet]
        [Route("languageCode")]
        public ActionResult GetSystemLanguageCode()
        {
            return new OkObjectResult(_languageCodeLogic.GetAll());
        }

        [HttpGet]
        [Route("languageCode/{systemLanguageCodeId}")]
        public ActionResult GetSystemLanguageCode(string systemLanguageCodeId)
        {
            SystemLanguageCodePoco item = _languageCodeLogic.Get(systemLanguageCodeId);
            if (item == null)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(item);
        }

        [HttpPost]
        [Route("languageCode")]
        public ActionResult PostSystemLanguageCode([FromBody] SystemLanguageCodePoco[] items)
        {
            try
            {
                _languageCodeLogic.Add(items);
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
        [Route("languageCode")]
        public ActionResult PutSystemLanguageCode([FromBody] SystemLanguageCodePoco[] items)
        {
            try
            {
                _languageCodeLogic.Update(items);
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
        [Route("languageCode")]
        public ActionResult DeleteSystemLanguageCode([FromBody] SystemLanguageCodePoco[] items)
        {
            _languageCodeLogic.Delete(items);
            return new OkResult();
        }
    }
}
