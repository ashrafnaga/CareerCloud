namespace CareerCloud.BusinessLogicLayer
{
    using CareerCloud.DataAccessLayer;
    using CareerCloud.Pocos;
    using System;
    using System.Collections.Generic;

    public class ApplicantEducationLogic : BaseLogic<ApplicantEducationPoco>
    {
        public ApplicantEducationLogic(IDataRepository<ApplicantEducationPoco> repository) : base(repository) { }

        protected override void Verify(ApplicantEducationPoco[] applicantEducations)
        {
            List<ValidationException> exceptionsList = new List<ValidationException>();

            foreach (ApplicantEducationPoco item in applicantEducations)
            {
                if (string.IsNullOrEmpty(item.Major) || item.Major.Length < 3)
                {
                    exceptionsList.Add(new ValidationException(107, "Major is blank or its lenght is less than 3 characters, kindly solve the issue."));
                }

                if (item.StartDate > DateTime.Now)
                {
                    exceptionsList.Add(new ValidationException(108, "Start Date Greater than current time, kindly solve the issue."));
                }

                if (item.CompletionDate < item.StartDate)
                {
                    exceptionsList.Add(new ValidationException(109, "Completion Date cannot be earlier than Start Date, kindly solve the issue."));
                }
            }

            if (exceptionsList.Count > 0)
            {
                throw new AggregateException(exceptionsList);
            }
        }

        public override void Add(ApplicantEducationPoco[] applicantEducations)
        {
            this.Verify(applicantEducations);
            base.Add(applicantEducations);
        }

        public override void Update(ApplicantEducationPoco[] applicantEducations)
        {
            this.Verify(applicantEducations);
            base.Update(applicantEducations);
        }
    }
}
