namespace CareerCloud.BusinessLogicLayer
{
    using CareerCloud.DataAccessLayer;
    using CareerCloud.Pocos;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class SystemCountryCodeLogic
    {
        protected IDataRepository<SystemCountryCodePoco> _repository;
        public SystemCountryCodeLogic(IDataRepository<SystemCountryCodePoco> repository)
        {
            _repository = repository;
        }
        protected void Verify(SystemCountryCodePoco[] systemCountryCodes)
        {
            List<ValidationException> exceptionsList = new List<ValidationException>();
            foreach (SystemCountryCodePoco item in systemCountryCodes)
            {
                if (string.IsNullOrEmpty(item.Code))
                {
                    exceptionsList.Add(new ValidationException(900, "Code Cannot be empty, kindly solve this issue."));
                }

                if (string.IsNullOrEmpty(item.Name))
                {
                    exceptionsList.Add(new ValidationException(901, "Name Cannot be empty, kindly solve this issue."));
                }
            }

            if (exceptionsList.Count > 0)
            {
                throw new AggregateException(exceptionsList);
            }
        }

        public List<SystemCountryCodePoco> GetAll()
        {
            return _repository.GetAll().ToList();
        }

        public SystemCountryCodePoco Get(string code)
        {
            return _repository.GetSingle(c => c.Code == code);
        }

        public void Add(SystemCountryCodePoco[] systemCountryCodes)
        {
            Verify(systemCountryCodes);
            _repository.Add(systemCountryCodes);
        }

        public void Update(SystemCountryCodePoco[] systemCountryCodes)
        {
            Verify(systemCountryCodes);
            _repository.Update(systemCountryCodes);
        }

        public void Delete(SystemCountryCodePoco[] systemCountryCodes)
        {
            _repository.Remove(systemCountryCodes);
        }
    }
}
