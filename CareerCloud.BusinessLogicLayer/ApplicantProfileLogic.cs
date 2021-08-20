namespace CareerCloud.BusinessLogicLayer
{
    using CareerCloud.DataAccessLayer;
    using CareerCloud.Pocos;
    using System;
    using System.Collections.Generic;
    public class ApplicantProfileLogic : BaseLogic<ApplicantProfilePoco>
    {
        public ApplicantProfileLogic(IDataRepository<ApplicantProfilePoco> repository) : base(repository) { }
        protected override void Verify(ApplicantProfilePoco[] applicantProfiles)
        {
            List<ValidationException> exceptionsList = new List<ValidationException>();
            foreach (ApplicantProfilePoco item in applicantProfiles)
            {
                if (item.CurrentSalary < 0)
                {
                    exceptionsList.Add(new ValidationException(111, "Current Salary cannot be negative, Kindly solve the issue."));
                }
                if (item.CurrentRate < 0)
                {
                    exceptionsList.Add(new ValidationException(112, "Current Rate cannot be negative, Kindly solve the issue."));
                }
            }

            if (exceptionsList.Count > 0)
            {
                throw new AggregateException(exceptionsList);
            }
        }

        public override void Add(ApplicantProfilePoco[] applicantProfiles)
        {
            this.Verify(applicantProfiles);
            base.Add(applicantProfiles);
        }
        public override void Update(ApplicantProfilePoco[] applicantProfiles)
        {
            this.Verify(applicantProfiles);
            base.Update(applicantProfiles);
        }
    }
}
