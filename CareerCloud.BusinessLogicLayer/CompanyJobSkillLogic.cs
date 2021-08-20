namespace CareerCloud.BusinessLogicLayer
{
    using CareerCloud.DataAccessLayer;
    using CareerCloud.Pocos;
    using System;
    using System.Collections.Generic;

    public class CompanyJobSkillLogic : BaseLogic<CompanyJobSkillPoco>
    {
        public CompanyJobSkillLogic(IDataRepository<CompanyJobSkillPoco> repository) : base(repository) { }
        protected override void Verify(CompanyJobSkillPoco[] companyJobSkills)
        {
            List<ValidationException> exceptionsList = new List<ValidationException>();
            foreach (CompanyJobSkillPoco item in companyJobSkills)
            {
                if (item.Importance < 0)
                {
                    exceptionsList.Add(new ValidationException(400, "Importance cannot be less than 0, kindly resolve this issue."));
                }
            }

            if (exceptionsList.Count > 0)
            {
                throw new AggregateException(exceptionsList);
            }
        }
        public override void Add(CompanyJobSkillPoco[] companyJobSkills)
        {
            this.Verify(companyJobSkills);
            base.Add(companyJobSkills);
        }
        public override void Update(CompanyJobSkillPoco[] companyJobSkills)
        {
            this.Verify(companyJobSkills);
            base.Update(companyJobSkills);
        }
    }
}
