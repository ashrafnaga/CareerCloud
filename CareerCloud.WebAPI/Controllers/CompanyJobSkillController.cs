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
    public class CompanyJobSkillController : ControllerBase
    {
        CompanyJobSkillLogic _jobSkillLogic;

        public CompanyJobSkillController()
        {
            var repository = new EFGenericRepository<CompanyJobSkillPoco>();
            _jobSkillLogic = new CompanyJobSkillLogic(repository);
        }

        [HttpGet]
        [Route("jobSkill")]
        public ActionResult GetCompanyJobSkill()
        {
            return new OkObjectResult(_jobSkillLogic.GetAll());
        }

        [HttpGet]
        [Route("jobSkill/{companyJobSkillId}")]
        public ActionResult GetCompanyJobSkill(Guid companyJobSkillId)
        {
            CompanyJobSkillPoco item = _jobSkillLogic.Get(companyJobSkillId);
            if (item == null)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(item);
        }

        [HttpPost]
        [Route("jobSkill")]
        public ActionResult PostCompanyJobSkill([FromBody] CompanyJobSkillPoco[] items)
        {
            try
            {
                _jobSkillLogic.Add(items);
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
        [Route("jobSkill")]
        public ActionResult PutCompanyJobSkill([FromBody] CompanyJobSkillPoco[] items)
        {
            try
            {
                _jobSkillLogic.Update(items);
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
        [Route("jobSkill")]
        public ActionResult DeleteCompanyJobSkill([FromBody] CompanyJobSkillPoco[] items)
        {
            _jobSkillLogic.Delete(items);
            return new OkResult();
        }
    }
}
