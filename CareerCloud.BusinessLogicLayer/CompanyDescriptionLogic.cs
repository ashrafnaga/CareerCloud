namespace CareerCloud.BusinessLogicLayer
{
    using CareerCloud.DataAccessLayer;
    using CareerCloud.Pocos;
    using System;
    using System.Collections.Generic;

    public class CompanyDescriptionLogic : BaseLogic<CompanyDescriptionPoco>
    {
        public CompanyDescriptionLogic(IDataRepository<CompanyDescriptionPoco> repository) : base(repository) { }
        protected override void Verify(CompanyDescriptionPoco[] companyDescriptions)
        {
            List<ValidationException> exceptionsList = new List<ValidationException>();
            foreach (CompanyDescriptionPoco item in companyDescriptions)
            {
                if (string.IsNullOrEmpty(item.CompanyDescription) || item.CompanyDescription.Length < 3)
                {
                    exceptionsList.Add(new ValidationException(107, "Company Description must be greater than 2 characters, kindly resolve this issue."));
                }

                if (string.IsNullOrEmpty(item.CompanyName) || item.CompanyName.Length < 3)
                {
                    exceptionsList.Add(new ValidationException(106, "Company Name must be greater than 2 characters, kindly resolve this issue."));
                }
            }

            if (exceptionsList.Count > 0)
            {
                throw new AggregateException(exceptionsList);
            }
        }
        public override void Add(CompanyDescriptionPoco[] companyDescriptions)
        {
            this.Verify(companyDescriptions);
            base.Add(companyDescriptions);
        }
        public override void Update(CompanyDescriptionPoco[] companyDescriptions)
        {
            this.Verify(companyDescriptions);
            base.Update(companyDescriptions);
        }
    }
}
