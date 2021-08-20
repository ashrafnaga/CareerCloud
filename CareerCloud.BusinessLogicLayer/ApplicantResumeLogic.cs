namespace CareerCloud.BusinessLogicLayer
{
    using CareerCloud.DataAccessLayer;
    using CareerCloud.Pocos;
    using System;
    using System.Collections.Generic;

    public class ApplicantResumeLogic : BaseLogic<ApplicantResumePoco>
    {
        public ApplicantResumeLogic(IDataRepository<ApplicantResumePoco> repository) : base(repository) { }
        protected override void Verify(ApplicantResumePoco[] applicantResumes)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            foreach (ApplicantResumePoco item in applicantResumes)
            {
                if (string.IsNullOrEmpty(item.Resume))
                {
                    exceptions.Add(new ValidationException(113, "Resume cannot be empty, Kindly solve the issue."));
                }
            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }
        public override void Add(ApplicantResumePoco[] applicantResumes)
        {
            this.Verify(applicantResumes);
            base.Add(applicantResumes);
        }
        public override void Update(ApplicantResumePoco[] applicantResumes)
        {
            this.Verify(applicantResumes);
            base.Update(applicantResumes);
        }
    }
}
