namespace CareerCloud.BusinessLogicLayer
{
    using CareerCloud.DataAccessLayer;
    using CareerCloud.Pocos;
    using System;
    using System.Collections.Generic;

    public class ApplicantSkillLogic : BaseLogic<ApplicantSkillPoco>
    {
        public ApplicantSkillLogic(IDataRepository<ApplicantSkillPoco> repository) : base(repository) { }
        protected override void Verify(ApplicantSkillPoco[] applicantSkills)
        {
            List<ValidationException> exceptionsList = new List<ValidationException>();
            foreach (ApplicantSkillPoco item in applicantSkills)
            {
                if (item.StartMonth > 12)
                {
                    exceptionsList.Add(new ValidationException(101, "Start Month Cannot be greater than 12, Kindly solve the issue."));
                }
                if (item.EndMonth > 12)
                {
                    exceptionsList.Add(new ValidationException(102, "End Month Cannot be greater than 12, Kindly solve the issue."));
                }
                if (item.StartYear < 1900)
                {
                    exceptionsList.Add(new ValidationException(103, "Start Year Cannot be less then 1900, Kindly solve the issue."));
                }
                if (item.EndYear < item.StartYear)
                {
                    exceptionsList.Add(new ValidationException(104, "End Year Cannot be less then Start Year, Kindly solve the issue."));
                }
            }

            if (exceptionsList.Count > 0)
            {
                throw new AggregateException(exceptionsList);
            }
        }
        public override void Add(ApplicantSkillPoco[] applicantSkills)
        {
            this.Verify(applicantSkills);
            base.Add(applicantSkills);
        }
        public override void Update(ApplicantSkillPoco[] applicantSkills)
        {
            this.Verify(applicantSkills);
            base.Update(applicantSkills);
        }
    }
}
