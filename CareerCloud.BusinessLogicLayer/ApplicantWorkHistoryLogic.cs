namespace CareerCloud.BusinessLogicLayer
{
    using CareerCloud.DataAccessLayer;
    using CareerCloud.Pocos;
    using System;
    using System.Collections.Generic;

    public class ApplicantWorkHistoryLogic : BaseLogic<ApplicantWorkHistoryPoco>
    {
        public ApplicantWorkHistoryLogic(IDataRepository<ApplicantWorkHistoryPoco> repository) : base(repository) { }
        protected override void Verify(ApplicantWorkHistoryPoco[] applicantWorkHistorys)
        {
            List<ValidationException> exceptionsList = new List<ValidationException>();
            foreach (ApplicantWorkHistoryPoco item in applicantWorkHistorys)
            {
                if (item.CompanyName.Length < 3)
                {
                    exceptionsList.Add(new ValidationException(105, "Must be greater then 2 characters"));
                }
            }

            if (exceptionsList.Count > 0)
            {
                throw new AggregateException(exceptionsList);
            }
        }
        public override void Add(ApplicantWorkHistoryPoco[] applicantWorkHistorys)
        {
            this.Verify(applicantWorkHistorys);
            base.Add(applicantWorkHistorys);
        }
        public override void Update(ApplicantWorkHistoryPoco[] applicantWorkHistorys)
        {
            this.Verify(applicantWorkHistorys);
            base.Update(applicantWorkHistorys);
        }
    }
}
