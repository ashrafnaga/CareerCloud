namespace CareerCloud.BusinessLogicLayer
{
    using CareerCloud.DataAccessLayer;
    using CareerCloud.Pocos;
    using System;
    using System.Collections.Generic;

    public class CompanyJobEducationLogic : BaseLogic<CompanyJobEducationPoco>
    {
        public CompanyJobEducationLogic(IDataRepository<CompanyJobEducationPoco> repository) : base(repository) { }
        protected override void Verify(CompanyJobEducationPoco[] companyJobEducations)
        {
            List<ValidationException> exceptionsList = new List<ValidationException>();
            foreach (CompanyJobEducationPoco item in companyJobEducations)
            {
                if (string.IsNullOrEmpty(item.Major) || item.Major.Length < 2)
                {
                    exceptionsList.Add(new ValidationException(200, "Major must be at least 2 characters, kindly resolve this issue."));
                }

                if (item.Importance < 0)
                {
                    exceptionsList.Add(new ValidationException(201, "Importance cannot be less than 0, kindly resolve this issue."));
                }
            }

            if (exceptionsList.Count > 0)
            {
                throw new AggregateException(exceptionsList);
            }
        }
        public override void Add(CompanyJobEducationPoco[] companyJobEducations)
        {
            this.Verify(companyJobEducations);
            base.Add(companyJobEducations);
        }
        public override void Update(CompanyJobEducationPoco[] companyJobEducations)
        {
            this.Verify(companyJobEducations);
            base.Update(companyJobEducations);
        }
    }
}
