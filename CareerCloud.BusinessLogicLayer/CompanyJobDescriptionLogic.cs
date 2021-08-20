namespace CareerCloud.BusinessLogicLayer
{
    using CareerCloud.DataAccessLayer;
    using CareerCloud.Pocos;
    using System;
    using System.Collections.Generic;

    public class CompanyJobDescriptionLogic : BaseLogic<CompanyJobDescriptionPoco>
    {
        public CompanyJobDescriptionLogic(IDataRepository<CompanyJobDescriptionPoco> repository) : base(repository) { }
        protected override void Verify(CompanyJobDescriptionPoco[] companyJobDescriptions)
        {
            List<ValidationException> exceptionsList = new List<ValidationException>();
            foreach (CompanyJobDescriptionPoco item in companyJobDescriptions)
            {
                if (string.IsNullOrEmpty(item.JobName))
                {
                    exceptionsList.Add(new ValidationException(300, "Job Name cannot be empty, kindly resolve this issue."));
                }

                if (string.IsNullOrEmpty(item.JobDescriptions))
                {
                    exceptionsList.Add(new ValidationException(301, "Job Descriptions cannot be empty, kindly resolve this issue."));
                }
            }

            if (exceptionsList.Count > 0)
            {
                throw new AggregateException(exceptionsList);
            }
        }
        public override void Add(CompanyJobDescriptionPoco[] companyJobDescriptions)
        {
            this.Verify(companyJobDescriptions);
            base.Add(companyJobDescriptions);
        }
        public override void Update(CompanyJobDescriptionPoco[] companyJobDescriptions)
        {
            this.Verify(companyJobDescriptions);
            base.Update(companyJobDescriptions);
        }
    }
}
