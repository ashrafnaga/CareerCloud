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
    public class CompanyJobController : ControllerBase
    {
        CompanyJobLogic _jobLogic;

        public CompanyJobController()
        {
            var repository = new EFGenericRepository<CompanyJobPoco>();
            _jobLogic = new CompanyJobLogic(repository);
        }

        [HttpGet]
        [Route("job")]
        public ActionResult GetCompanyJob()
        {
            return new OkObjectResult(_jobLogic.GetAll());
        }

        [HttpGet]
        [Route("job/{companyJobId}")]
        public ActionResult GetCompanyJob(Guid companyJobId)
        {
            CompanyJobPoco item = _jobLogic.Get(companyJobId);
            if (item == null)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(item);
        }

        [HttpPost]
        [Route("job")]
        public ActionResult PostCompanyJob([FromBody] CompanyJobPoco[] items)
        {
            try
            {
                _jobLogic.Add(items);
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
        [Route("job")]
        public ActionResult PutCompanyJob([FromBody] CompanyJobPoco[] items)
        {
            try
            {
                _jobLogic.Update(items);
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
        [Route("job")]
        public ActionResult DeleteCompanyJob([FromBody] CompanyJobPoco[] items)
        {
            _jobLogic.Delete(items);
            return new OkResult();
        }
    }
}
