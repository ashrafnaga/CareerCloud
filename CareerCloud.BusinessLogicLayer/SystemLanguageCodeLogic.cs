namespace CareerCloud.BusinessLogicLayer
{
    using CareerCloud.DataAccessLayer;
    using CareerCloud.Pocos;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class SystemLanguageCodeLogic
    {
        protected IDataRepository<SystemLanguageCodePoco> _repository;
        public SystemLanguageCodeLogic(IDataRepository<SystemLanguageCodePoco> repository)
        {
            _repository = repository;
        }

        protected virtual void Verify(SystemLanguageCodePoco[] systemLanguageCodes)
        {
            List<ValidationException> exceptionsList = new List<ValidationException>();
            foreach (SystemLanguageCodePoco item in systemLanguageCodes)
            {
                if (string.IsNullOrEmpty(item.LanguageID))
                {
                    exceptionsList.Add(new ValidationException(1000, "ID Cannot be empty, kindly solve the issue."));
                }

                if (string.IsNullOrEmpty(item.Name))
                {
                    exceptionsList.Add(new ValidationException(1001, "Name Cannot be empty, kindly solve the issue."));
                }

                if (string.IsNullOrEmpty(item.NativeName))
                {
                    exceptionsList.Add(new ValidationException(1002, "Native Name Cannot be empty, kindly solve the issue."));
                }
            }

            if (exceptionsList.Count > 0)
            {
                throw new AggregateException(exceptionsList);
            }
        }

        public SystemLanguageCodePoco Get(string languageid)
        {
            return _repository.GetSingle(c => c.LanguageID == languageid);
        }

        public virtual List<SystemLanguageCodePoco> GetAll()
        {
            return _repository.GetAll().ToList();
        }



        public virtual void Add(SystemLanguageCodePoco[] systemLanguageCodes)
        {

            Verify(systemLanguageCodes);
            _repository.Add(systemLanguageCodes);
        }

        public virtual void Update(SystemLanguageCodePoco[] systemLanguageCodes)
        {
            Verify(systemLanguageCodes);
            _repository.Update(systemLanguageCodes);
        }

        public void Delete(SystemLanguageCodePoco[] systemLanguageCodes)
        {
            _repository.Remove(systemLanguageCodes);
        }
    }
}
