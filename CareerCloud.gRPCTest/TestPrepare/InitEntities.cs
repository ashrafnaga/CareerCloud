using CareerCloud.gRPCTest.Services;
using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.gRPCTest.TestPrepare
{
    public class InitEntities
    {
        public SystemLanguageCodeProto _systemLangCode;
        public CompanyDescriptionProto _companyDescription;
        public CompanyJobProto _companyJob;
        public CompanyJobEducationProto _companyJobEducation;
        public SecurityLoginProto _securityLogin;
        public ApplicantProfileProto _applicantProfile;
        public SecurityLoginsLogProto _securityLoginLog;
        public ApplicationEducationProto _applicantEducation;
        public ApplicantJobApplicationProto _applicantJobApplication;



        public InitEntities()
        {
            SystemLangCode_Init();
            CompanyDescription_Init();
            CompanyJob_Init();
            CompanyJobEducation_Init();
            SecurityLogin_Init();
            ApplicantProfile_Init();
            SecurityLoginLog_Init();
            ApplicantEducation_Init();
            ApplicantJobApplication_Init();

        }

        private void SystemLangCode_Init()
        {
            _systemLangCode = new SystemLanguageCodeProto()
            {
                LanguageId = String.Concat(Faker.Lorem.Letters(10)),
                NativeName = Truncate(Faker.Lorem.Sentence(), 50),
                Name = Truncate(Faker.Lorem.Sentence(), 50)
            };
        }

        private void CompanyDescription_Init()
        {
            _companyDescription = new CompanyDescriptionProto()
            {
                Id = Guid.NewGuid().ToString(),
                CompanyDescription = Faker.Company.CatchPhrase(),
                CompanyName = Faker.Company.CatchPhrasePos(),
                LanguageId = _systemLangCode.LanguageId,
                Company = "178AACB1-C64B-425D-1018-DAE0B908D807"
            };
        }

        private void CompanyJob_Init()
        {
            _companyJob = new CompanyJobProto()
            {
                Id = Guid.NewGuid().ToString(),
                Company = "178AACB1-C64B-425D-1018-DAE0B908D807",
                IsCompanyHidden = false,
                IsInactive = false,
                ProfileCreated = Timestamp.FromDateTimeOffset(Faker.Date.Past()) 
            };
        }

        private void CompanyJobEducation_Init()
        {
            _companyJobEducation = new CompanyJobEducationProto()
            {
                Id = Guid.NewGuid().ToString(),
                Importance = 2,
                Job = _companyJob.Id,
                Major = Truncate(Faker.Lorem.Sentence(), 100)
            };
        }

        private void SecurityLogin_Init()
        {
            _securityLogin = new SecurityLoginProto()
            {
                Login = Faker.User.Email(),
                AgreementAccepted = Timestamp.FromDateTimeOffset(Faker.Date.PastWithTime()),
                Created = Timestamp.FromDateTimeOffset(Faker.Date.PastWithTime()),
                EmailAddress = Faker.User.Email(),
                ForceChangePassword = false,
                FullName = Faker.Name.FullName(),
                Id = Guid.NewGuid().ToString(),
                IsInactive = false,
                IsLocked = false,
                Password = "SoMePassWord#&@",
                PasswordUpdate = Timestamp.FromDateTimeOffset(Faker.Date.Forward()),
                PhoneNumber = "555-416-9889",
                PrefferredLanguage = "EN".PadRight(10)
            };
        }
        
        private void ApplicantProfile_Init()
        {
            _applicantProfile = new ApplicantProfileProto
            {
                Id = Guid.NewGuid().ToString(),
                Login = _securityLogin.Id,
                City = Faker.Address.CityPrefix(),
                Country = "CNWQZukGiS",
                Currency = "CAN".PadRight(10),
                CurrentRate = 71.25,
                CurrentSalary = 67500,
                Province = Truncate(Faker.Address.Province(), 10).PadRight(10),
                Street = Truncate(Faker.Address.StreetName(), 100),
                PostalCode = Truncate(Faker.Address.CanadianZipCode(), 20).PadRight(20)
            };
        }

        private void SecurityLoginLog_Init()
        {
            _securityLoginLog = new SecurityLoginsLogProto()
            {
                Id = Guid.NewGuid().ToString(),
                IsSuccesful = true,
                Login = _securityLogin.Id,
                LogonDate = Timestamp.FromDateTimeOffset(Faker.Date.PastWithTime()),
                SourceIp = Faker.Internet.IPv4().PadRight(15)
            };
        }

        private void ApplicantEducation_Init()
        {
            _applicantEducation = new ApplicationEducationProto()
            {
                Id = Guid.NewGuid().ToString(),
                Applicant = _applicantProfile.Id,
                Major = Faker.Education.Major(),
                CertificateDiploma = Faker.Education.Major(),
                StartDate = Timestamp.FromDateTimeOffset(Faker.Date.Past(3)),
                CompletionDate = Timestamp.FromDateTimeOffset(Faker.Date.Forward(1)),
                CompletionPercent = Google.Protobuf.ByteString.CopyFrom(new byte[] { (byte)Faker.Number.RandomNumber(1) })
            };
        }

        private void ApplicantJobApplication_Init()
        {
            _applicantJobApplication = new ApplicantJobApplicationProto()
            {
                Id = Guid.NewGuid().ToString(),
                ApplicationDate = Timestamp.FromDateTimeOffset(Faker.Date.Recent()),
                Applicant = _applicantProfile.Id,
                Job = _companyJob.Id
            };
        }





        public string Truncate(string str, int maxLength)
        {
            if (string.IsNullOrEmpty(str)) return str;
            return str.Length <= maxLength ? str : str.Substring(0, maxLength);
        }
    }
}
