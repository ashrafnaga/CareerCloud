namespace CareerCloud.WebAPI.Controllers
{
    using CareerCloud.BusinessLogicLayer;
    using CareerCloud.EntityFrameworkDataAccess;
    using CareerCloud.Pocos;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [Route("api/careercloud/company/v1")]
    [ApiController]
    public class CompanyJobEducationController : ControllerBase
    {
        CompanyJobEducationLogic _jobEducationLogic;

        public CompanyJobEducationController()
        {
            var repository = new EFGenericRepository<CompanyJobEducationPoco>();
            _jobEducationLogic = new CompanyJobEducationLogic(repository);
        }

        [HttpGet]
        [Route("jobEducation")]
        public ActionResult GetCompanyJobEducation()
        {
            return new OkObjectResult(_jobEducationLogic.GetAll());
        }

        [HttpGet]
        [Route("jobEducation/{companyJobEducationId}")]
        public ActionResult GetCompanyJobEducation(Guid companyJobEducationId)
        {
            CompanyJobEducationPoco item = _jobEducationLogic.Get(companyJobEducationId);
            if (item == null)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(item);
        }

        [HttpPost]
        [Route("jobEducation")]
        public ActionResult PostCompanyJobEducation([FromBody] CompanyJobEducationPoco[] items)
        {
            try
            {
                _jobEducationLogic.Add(items);
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
        [Route("jobEducation")]
        public ActionResult PutCompanyJobEducation([FromBody] CompanyJobEducationPoco[] items)
        {
            try
            {
                _jobEducationLogic.Update(items);
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
        [Route("jobEducation")]
        public ActionResult DeleteCompanyJobEducation([FromBody] CompanyJobEducationPoco[] items)
        {
            _jobEducationLogic.Delete(items);
            return new OkResult();
        }
    }
}
