namespace CareerCloud.BusinessLogicLayer
{
    using CareerCloud.DataAccessLayer;
    using CareerCloud.Pocos;
    using System;
    using System.Collections.Generic;

    public class CompanyLocationLogic : BaseLogic<CompanyLocationPoco>
    {
        public CompanyLocationLogic(IDataRepository<CompanyLocationPoco> repository) : base(repository) { }
        protected override void Verify(CompanyLocationPoco[] companyLocations)
        {
            List<ValidationException> exceptionsList = new List<ValidationException>();
            foreach (CompanyLocationPoco item in companyLocations)
            {
                if (string.IsNullOrEmpty(item.CountryCode))
                {
                    exceptionsList.Add(new ValidationException(500, "Country Code cannot be empty, kindly resolve this issue."));
                }

                if (string.IsNullOrEmpty(item.Province))
                {
                    exceptionsList.Add(new ValidationException(501, "Province cannot be empty, kindly resolve this issue."));
                }

                if (string.IsNullOrEmpty(item.Street))
                {
                    exceptionsList.Add(new ValidationException(502, "Street cannot be empty, kindly resolve this issue."));
                }

                if (string.IsNullOrEmpty(item.City))
                {
                    exceptionsList.Add(new ValidationException(503, "City cannot be empty, kindly resolve this issue."));
                }

                if (string.IsNullOrEmpty(item.PostalCode))
                {
                    exceptionsList.Add(new ValidationException(504, "Postal Code cannot be empty, kindly resolve this issue."));
                }
            }

            if (exceptionsList.Count > 0)
            {
                throw new AggregateException(exceptionsList);
            }
        }
        public override void Add(CompanyLocationPoco[] companyLocations)
        {
            this.Verify(companyLocations);
            base.Add(companyLocations);
        }
        public override void Update(CompanyLocationPoco[] companyLocations)
        {
            this.Verify(companyLocations);
            base.Update(companyLocations);
        }
    }
}
