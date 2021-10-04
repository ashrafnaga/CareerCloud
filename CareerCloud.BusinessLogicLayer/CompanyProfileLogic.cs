namespace CareerCloud.BusinessLogicLayer
{
    using CareerCloud.DataAccessLayer;
    using CareerCloud.Pocos;
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    public class CompanyProfileLogic : BaseLogic<CompanyProfilePoco>
    {
        public CompanyProfileLogic(IDataRepository<CompanyProfilePoco> repository) : base(repository) { }
        protected override void Verify(CompanyProfilePoco[] companyProfiles)
        {
            List<ValidationException> exceptionsList = new List<ValidationException>();
            foreach (CompanyProfilePoco item in companyProfiles)
            {
                if (!string.IsNullOrEmpty(item.CompanyWebsite) && (
                    item.CompanyWebsite.Contains(".com")
                    || item.CompanyWebsite.Contains(".biz")
                    || item.CompanyWebsite.Contains(".ca"))) { }
                else
                {
                    exceptionsList.Add(new ValidationException(600, "Valid websites must end with extensions: '.com', '.biz', or '.ca'"));
                }


                string MatchPhonePattern =@"\d{3}-\d{3}-\d{4}";
                if (string.IsNullOrEmpty(item.ContactPhone) || !Regex.Match(item.ContactPhone, MatchPhonePattern).Success)
                {
                    exceptionsList.Add(new ValidationException(601, $"Phone Number for contact {item.ContactName} require a right format like => '416-555-1234'."));
                }
            }

            if (exceptionsList.Count > 0)
            {
                throw new AggregateException(exceptionsList);
            }
        }

        public override void Add(CompanyProfilePoco[] companyProfiles)
        {
            Verify(companyProfiles);
            base.Add(companyProfiles);
        }
        public override void Update(CompanyProfilePoco[] companyProfiles)
        {
            Verify(companyProfiles);
            base.Update(companyProfiles);
        }
    }
}
