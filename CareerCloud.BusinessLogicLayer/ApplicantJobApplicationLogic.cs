namespace CareerCloud.BusinessLogicLayer
{
    using CareerCloud.DataAccessLayer;
    using CareerCloud.Pocos;
    using System;
    using System.Collections.Generic;

    public class ApplicantJobApplicationLogic : BaseLogic<ApplicantJobApplicationPoco>
    {
        public ApplicantJobApplicationLogic(IDataRepository<ApplicantJobApplicationPoco> repository) : base(repository) { }
        protected override void Verify(ApplicantJobApplicationPoco[] applicantJobApplications)
        {
            List<ValidationException> exceptionsList = new List<ValidationException>();
            foreach (ApplicantJobApplicationPoco item in applicantJobApplications)
            {
                if (item.ApplicationDate > DateTime.Now)
                {
                    exceptionsList.Add(new ValidationException(110, "Application Date cannot be greater than current data, Kindly solve the issue."));
                }
            }

            if (exceptionsList.Count > 0)
            {
                throw new AggregateException(exceptionsList);
            }
        }
        public override void Add(ApplicantJobApplicationPoco[] applicantJobApplications)
        {
            this.Verify(applicantJobApplications);
            base.Add(applicantJobApplications);
        }
        public override void Update(ApplicantJobApplicationPoco[] applicantJobApplications)
        {
            this.Verify(applicantJobApplications);
            base.Update(applicantJobApplications);
        }
    }
}
