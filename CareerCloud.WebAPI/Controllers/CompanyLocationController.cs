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
    public class CompanyLocationController : ControllerBase
    {
        CompanyLocationLogic _locationLogic;

        public CompanyLocationController()
        {
            var repository = new EFGenericRepository<CompanyLocationPoco>();
            _locationLogic = new CompanyLocationLogic(repository);
        }

        [HttpGet]
        [Route("location")]
        public ActionResult GetCompanyLocation()
        {
            return new OkObjectResult(_locationLogic.GetAll());
        }

        [HttpGet]
        [Route("location/{companyLocationId}")]
        public ActionResult GetCompanyLocation(Guid companyLocationId)
        {
            CompanyLocationPoco item = _locationLogic.Get(companyLocationId);
            if (item == null)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(item);
        }

        [HttpPost]
        [Route("location")]
        public ActionResult PostCompanyLocation([FromBody] CompanyLocationPoco[] items)
        {
            try
            {
                _locationLogic.Add(items);
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
        [Route("location")]
        public ActionResult PutCompanyLocation([FromBody] CompanyLocationPoco[] items)
        {
            try
            {
                _locationLogic.Update(items);
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
        [Route("location")]
        public ActionResult DeleteCompanyLocation([FromBody] CompanyLocationPoco[] items)
        {
            _locationLogic.Delete(items);
            return new OkResult();
        }
    }
}
