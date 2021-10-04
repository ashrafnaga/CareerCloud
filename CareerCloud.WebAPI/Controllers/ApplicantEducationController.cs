namespace CareerCloud.WebAPI.Controllers
{
    using CareerCloud.BusinessLogicLayer;
    using CareerCloud.EntityFrameworkDataAccess;
    using CareerCloud.Pocos;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [Route("api/careercloud/applicant/v1")]
    [ApiController]
    public class ApplicantEducationController : ControllerBase
    {
        ApplicantEducationLogic _applicationEducationLogic;

        public ApplicantEducationController()
        {
            var repository = new EFGenericRepository<ApplicantEducationPoco>();
            _applicationEducationLogic = new ApplicantEducationLogic(repository);
        }

        [HttpGet]
        [Route("education")]
        public ActionResult<List<ApplicantEducationPoco>> GetApplicantEducation()
        {
            return new OkObjectResult(_applicationEducationLogic.GetAll());
        }

        [HttpGet]
        [Route("education/{applicantEducationId}")]
        public ActionResult GetApplicantEducation(Guid applicantEducationId)
        {
            ApplicantEducationPoco item = _applicationEducationLogic.Get(applicantEducationId);
            if(item == null)
            {
                return new NotFoundResult();
            }
            
            return new OkObjectResult(item);
        }

        [HttpPost]
        [Route("education")]
        public ActionResult PostApplicantEducation([FromBody] ApplicantEducationPoco[] items)
        {
            try
            {
                _applicationEducationLogic.Add(items);
                return new OkResult();
            }
            catch(AggregateException ex)
            {
                Dictionary<int, string> validationErrors = new Dictionary<int, string>();
                foreach(var exeption in ex.InnerExceptions)
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
        [Route("education")]
        public ActionResult PutApplicantEducation([FromBody] ApplicantEducationPoco[] items)
        {
            try
            {
                _applicationEducationLogic.Update(items);
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
        [Route("education")]
        public ActionResult DeleteApplicantEducation([FromBody] ApplicantEducationPoco[] items)
        {
            _applicationEducationLogic.Delete(items);

            return new OkResult();
        }

    }
}
