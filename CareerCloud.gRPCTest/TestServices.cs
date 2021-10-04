using CareerCloud.gRPCTest.Services;
using CareerCloud.gRPCTest.Common;
using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareerCloud.gRPCTest.TestPrepare;

namespace CareerCloud.gRPCTest
{
    public class TestServices : InitEntities
    {
        private readonly SystemLanguageCodeProvider.SystemLanguageCodeProviderClient _systemLanguageCodeClient;
        private readonly CompanyDescriptionProvider.CompanyDescriptionProviderClient _companyDescriptionClient;
        private readonly CompanyJobProvider.CompanyJobProviderClient _companyJobClient;
        private readonly CompanyJobEducationProvider.CompanyJobEducationProviderClient _companyJobEducationClient;
        private readonly SecurityLoginProvider.SecurityLoginProviderClient _securityLoginClient;
        private readonly SecurityLoginsLogProvider.SecurityLoginsLogProviderClient _securityLoginsLogClient;
        private readonly ApplicantProfileProvider.ApplicantProfileProviderClient _applicantProfileClient;
        private readonly ApplicantEducationProvider.ApplicantEducationProviderClient _applicantEducationClient;
        private readonly ApplicantJobApplicationProvider.ApplicantJobApplicationProviderClient _applicantJobApplicationClient;

        public TestServices() : base()
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");


            _systemLanguageCodeClient = new SystemLanguageCodeProvider.SystemLanguageCodeProviderClient(channel);
            _companyDescriptionClient = new CompanyDescriptionProvider.CompanyDescriptionProviderClient(channel);
            _companyJobClient = new CompanyJobProvider.CompanyJobProviderClient(channel);
            _companyJobEducationClient = new CompanyJobEducationProvider.CompanyJobEducationProviderClient(channel);
            _securityLoginClient = new SecurityLoginProvider.SecurityLoginProviderClient(channel);
            _securityLoginsLogClient = new SecurityLoginsLogProvider.SecurityLoginsLogProviderClient(channel);
            _applicantProfileClient = new ApplicantProfileProvider.ApplicantProfileProviderClient(channel);
            _applicantEducationClient = new ApplicantEducationProvider.ApplicantEducationProviderClient(channel);
            _applicantJobApplicationClient = new ApplicantJobApplicationProvider.ApplicantJobApplicationProviderClient(channel);


        }


        public bool StartTest()
        {
            Console.WriteLine($"Tests Started.");
            Console.WriteLine($"----------------------------");

            bool result = TestSystemLanguageCode();
            if (result == false)
                return false;

            result = TestCompanyDescription();
            if (result == false)
                return false;

            result = TestCompanyJob();
            if (result == false)
                return false;

            result = TestCompanyJobEducation();
            if (result == false)
                return false;

            result = TestSecurityLogin();
            if (result == false)
                return false;

            result = TestSecurityLoginsLog();
            if (result == false)
                return false;

            result = TestApplicantProfile();
            if (result == false)
                return false;

            result = TestApplicantEducation();
            if (result == false)
                return false;

            result = TestApplicantJobApplication();
            if (result == false)
                return false;

            Console.WriteLine();
            Console.WriteLine($"----------------------------");
            Console.WriteLine($"All Tests Done Successfully.");

            Console.WriteLine();
            Console.WriteLine($"Cleaning Started.");
            Console.WriteLine($"----------------------------");
            result = ApplicantJobApplicationRemove();
            if (result == false)
                return false;

            result = ApplicantEducationRemove();
            if (result == false)
                return false;

            result = ApplicantProfileRemove();
            if (result == false)
                return false;

            result = SecurityLoginLogRemove();
            if (result == false)
                return false;

            result = SecurityLoginRemove();
            if (result == false)
                return false;

            result = CompanyJobEducationRemove();
            if (result == false)
                return false;

            result = CompanyJobRemove();
            if (result == false)
                return false;

            result = CompanyDescriptionRemove();
            if (result == false)
                return false;

            result = SystemLanguageCodeRemove();
            if (result == false)
                return false;

            Console.WriteLine();
            Console.WriteLine($"----------------------------");
            Console.WriteLine($"Cleaning Done Successfully.");
            return true;
        }


        #region SystemLanguageCode Test
        public bool TestSystemLanguageCode()
        {
            bool result = SystemLanguageCodeAdd();
            if (result == false)
                return false;
            result = SystemLanguageCodeCheck();
            if (result == false)
                return false;
            result = SystemLanguageCodeUpdate();
            if (result == false)
                return false;
            result = SystemLanguageCodeCheck();
            if (result == false)
                return false;

            Console.WriteLine($"System Language Code Test Done Successfully.");
            return true;
        }

        private bool SystemLanguageCodeAdd()
        {
            var list = new SystemLanguageCodeList();
            list.SystemLanguageCodes.Add(_systemLangCode);

            BoolReply result = _systemLanguageCodeClient.PostSystemLanguageCode(list);

            if (result.Result == false)
                Console.WriteLine($"System Language Code Add Error: {string.Join(", ", result.Errors.Select(e => e.ErrorMessage))}");

            return result.Result;
        }

        private bool SystemLanguageCodeCheck()
        {
            IdRequest request = new IdRequest();
            request.Id = _systemLangCode.LanguageId;

            SystemLanguageCodeProto result = _systemLanguageCodeClient.GetSystemLanguageCode(request);

            if (result.CalculateSize() <= 0)
            {
                Console.WriteLine($"System Language Code Check Error: Null result");
                return false;
            }

            if (_systemLangCode.LanguageId != result.LanguageId)
            {
                Console.WriteLine($"System Language Code Check Error: Wrong LanguageId Field");
                return false;
            }

            if (_systemLangCode.Name != result.Name)
            {
                Console.WriteLine($"System Language Code Check Error: Wrong Name Field");
                return false;
            }

            if (_systemLangCode.NativeName != result.NativeName)
            {
                Console.WriteLine($"System Language Code Check Error: Wrong NativeName Field");
                return false;
            }

            return true;
        }

        public bool SystemLanguageCodeUpdate()
        {
            _systemLangCode.NativeName = Truncate(Faker.Lorem.Sentence(), 50);
            _systemLangCode.Name = Truncate(Faker.Lorem.Sentence(), 50);


            var list = new SystemLanguageCodeList();
            list.SystemLanguageCodes.Add(_systemLangCode);

            BoolReply result = _systemLanguageCodeClient.PutSystemLanguageCode(list);

            if (result.Result == false)
                Console.WriteLine($"System Language Code Update Error: {string.Join(", ", result.Errors.Select(e => e.ErrorMessage))}");

            return result.Result;
        }

        #endregion

        #region CompanyDescription Test
        public bool TestCompanyDescription()
        {
            bool result = CompanyDescriptionAdd();
            if (result == false)
                return false;
            result = CompanyDescriptionCheck();
            if (result == false)
                return false;
            result = CompanyDescriptionUpdate();
            if (result == false)
                return false;
            result = CompanyDescriptionCheck();
            if (result == false)
                return false;

            Console.WriteLine($"CompanyDescription Test Done Successfully.");
            return true;
        }

        private bool CompanyDescriptionAdd()
        {
            var list = new CompanyDescriptionList();
            list.CompanyDescriptions.Add(_companyDescription);

            BoolReply result = _companyDescriptionClient.PostCompanyDescription(list);

            if (result.Result == false)
                Console.WriteLine($"Company Description Add Error: {string.Join(", ", result.Errors.Select(e => e.ErrorMessage))}");

            return result.Result;
        }

        private bool CompanyDescriptionCheck()
        {
            IdRequest request = new IdRequest();
            request.Id = _companyDescription.Id;

            CompanyDescriptionProto result = _companyDescriptionClient.GetCompanyDescription(request);

            if (result.CalculateSize() <= 0)
            {
                Console.WriteLine($"Company Description Check Error: Null result");
                return false;
            }

            if (_companyDescription.Id != result.Id)
            {
                Console.WriteLine($"Company Description Check Error: Wrong Id Field");
                return false;
            }

            if (_companyDescription.Company.ToLower() != result.Company.ToLower())
            {
                Console.WriteLine($"Company Description Check Error: Wrong Company Field");
                return false;
            }

            if (_companyDescription.CompanyDescription != result.CompanyDescription)
            {
                Console.WriteLine($"Company Description Check Error: Wrong CompanyDescription Field");
                return false;
            }

            if (_companyDescription.CompanyName != result.CompanyName)
            {
                Console.WriteLine($"Company Description Check Error: Wrong CompanyName Field");
                return false;
            }

            if (_companyDescription.LanguageId != result.LanguageId)
            {
                Console.WriteLine($"Company Description Check Error: Wrong LanguageId Field");
                return false;
            }

            return true;
        }

        public bool CompanyDescriptionUpdate()
        {
            _companyDescription.CompanyDescription = Faker.Company.CatchPhrase();
            _companyDescription.CompanyName = Faker.Company.CatchPhrasePos();


            var list = new CompanyDescriptionList();
            list.CompanyDescriptions.Add(_companyDescription);

            BoolReply result = _companyDescriptionClient.PutCompanyDescription(list);

            if (result.Result == false)
                Console.WriteLine($"Company Description Update Error: {string.Join(", ", result.Errors.Select(e => e.ErrorMessage))}");

            return result.Result;
        }

        #endregion

        #region CompanyJob Test
        public bool TestCompanyJob()
        {
            bool result = CompanyJobAdd();
            if (result == false)
                return false;
            result = CompanyJobCheck();
            if (result == false)
                return false;
            result = CompanyJobUpdate();
            if (result == false)
                return false;
            result = CompanyJobCheck();
            if (result == false)
                return false;

            Console.WriteLine($"CompanyJob Test Done Successfully.");
            return true;
        }

        private bool CompanyJobAdd()
        {
            var list = new CompanyJobList();
            list.CompanyJobs.Add(_companyJob);

            BoolReply result = _companyJobClient.PostCompanyJob(list);

            if (result.Result == false)
                Console.WriteLine($"Company Job Add Error: {string.Join(", ", result.Errors.Select(e => e.ErrorMessage))}");

            return result.Result;
        }

        private bool CompanyJobCheck()
        {
            IdRequest request = new IdRequest();
            request.Id = _companyJob.Id;

            CompanyJobProto result = _companyJobClient.GetCompanyJob(request);

            if (result.CalculateSize() <= 0)
            {
                Console.WriteLine($"Company Job Check Error: Null result");
                return false;
            }

            if (_companyJob.Id != result.Id)
            {
                Console.WriteLine($"Company Job Check Error: Wrong Id Field");
                return false;
            }

            if (_companyJob.Company.ToLower() != result.Company.ToLower())
            {
                Console.WriteLine($"Company Job Check Error: Wrong Company Field");
                return false;
            }

            if (_companyJob.IsCompanyHidden != result.IsCompanyHidden)
            {
                Console.WriteLine($"Company Job Check Error: Wrong IsCompanyHidden Field");
                return false;
            }

            if (_companyJob.IsInactive != result.IsInactive)
            {
                Console.WriteLine($"Company Job Check Error: Wrong IsInactive Field");
                return false;
            }

            DateTimeOffset offsetProfileCreated = result.ProfileCreated.ToDateTimeOffset();
            Timestamp ProfileCreated = Timestamp.FromDateTimeOffset(offsetProfileCreated);

            if (_companyJob.ProfileCreated != ProfileCreated)
            {
                Console.WriteLine($"Company Job Check Error: Wrong ProfileCreated Field");
                return false;
            }

            return true;
        }

        public bool CompanyJobUpdate()
        {
            _companyJob.IsCompanyHidden = true;
            _companyJob.IsInactive = true;
            _companyJob.ProfileCreated = Timestamp.FromDateTimeOffset(Faker.Date.Past());


            var list = new CompanyJobList();
            list.CompanyJobs.Add(_companyJob);

            BoolReply result = _companyJobClient.PutCompanyJob(list);

            if (result.Result == false)
                Console.WriteLine($"Company Job Update Error: {string.Join(", ", result.Errors.Select(e => e.ErrorMessage))}");

            return result.Result;
        }

        #endregion

        #region CompanyJobEducation Test
        public bool TestCompanyJobEducation()
        {
            bool result = CompanyJobEducationAdd();
            if (result == false)
                return false;
            result = CompanyJobEducationCheck();
            if (result == false)
                return false;
            result = CompanyJobEducationUpdate();
            if (result == false)
                return false;
            result = CompanyJobEducationCheck();
            if (result == false)
                return false;

            Console.WriteLine($"CompanyJobEducation Test Done Successfully.");
            return true;
        }

        private bool CompanyJobEducationAdd()
        {
            var list = new CompanyJobEducationList();
            list.CompanyJobEducations.Add(_companyJobEducation);

            BoolReply result = _companyJobEducationClient.PostCompanyJobEducation(list);

            if (result.Result == false)
                Console.WriteLine($"Company Job Education Add Error: {string.Join(", ", result.Errors.Select(e => e.ErrorMessage))}");

            return result.Result;
        }

        private bool CompanyJobEducationCheck()
        {
            IdRequest request = new IdRequest();
            request.Id = _companyJobEducation.Id;

            CompanyJobEducationProto result = _companyJobEducationClient.GetCompanyJobEducation(request);

            if (result.CalculateSize() <= 0)
            {
                Console.WriteLine($"Company Job Education Check Error: Null result");
                return false;
            }

            if (_companyJobEducation.Id != result.Id)
            {
                Console.WriteLine($"Company Job Education Check Error: Wrong Id Field");
                return false;
            }

            if (_companyJobEducation.Importance != result.Importance)
            {
                Console.WriteLine($"Company Job Education Check Error: Wrong Importance Field");
                return false;
            }

            if (_companyJobEducation.Job != result.Job)
            {
                Console.WriteLine($"Company Job Education Check Error: Wrong Importance Field");
                return false;
            }

            return true;
        }

        public bool CompanyJobEducationUpdate()
        {
            _companyJobEducation.Importance = 1;
            _companyJobEducation.Major = Truncate(Faker.Lorem.Sentence(), 100);

            var list = new CompanyJobEducationList();
            list.CompanyJobEducations.Add(_companyJobEducation);

            BoolReply result = _companyJobEducationClient.PutCompanyJobEducation(list);

            if (result.Result == false)
                Console.WriteLine($"Company Job Education Update Error: {string.Join(", ", result.Errors.Select(e => e.ErrorMessage))}");

            return result.Result;
        }

        #endregion

        #region SecurityLogin Test
        public bool TestSecurityLogin()
        {
            bool result = SecurityLoginAdd();
            if (result == false)
                return false;
            result = SecurityLoginCheck();
            if (result == false)
                return false;
            result = SecurityLoginUpdate();
            if (result == false)
                return false;
            result = SecurityLoginCheck();
            if (result == false)
                return false;

            Console.WriteLine($"SecurityLogin Test Done Successfully.");
            return true;
        }

        private bool SecurityLoginAdd()
        {
            var securityLoginList = new SecurityLoginList();
            securityLoginList.SecurityLogins.Add(_securityLogin);

            BoolReply result = _securityLoginClient.PostSecurityLogin(securityLoginList);

            if (result.Result == false)
                Console.WriteLine($"Security Login Add Error: {string.Join(", ", result.Errors.Select(e => e.ErrorMessage))}");

            return result.Result;
        }

        private bool SecurityLoginCheck()
        {
            IdRequest request = new IdRequest();
            request.Id = _securityLogin.Id;

            SecurityLoginProto result = _securityLoginClient.GetSecurityLogin(request);

            if (result.CalculateSize() <= 0)
            {
                Console.WriteLine($"Security Login Check Error: Null result");
                return false;
            }

            if (_securityLogin.Id != result.Id)
            {
                Console.WriteLine($"Security Login Check Error: Wrong Id Field");
                return false;
            }

            if (_securityLogin.Login != result.Login)
            {
                Console.WriteLine($"Security Login Check Error: Wrong Login Field");
                return false;
            }

            if (_securityLogin.AgreementAccepted != result.AgreementAccepted)
            {
                Console.WriteLine($"Security Login Check Error: Wrong AgreementAccepted Field");
                return false;
            }

            if (_securityLogin.EmailAddress != result.EmailAddress)
            {
                Console.WriteLine($"Security Login Check Error: Wrong EmailAddress Field");
                return false;
            }

            if (_securityLogin.PhoneNumber != result.PhoneNumber)
            {
                Console.WriteLine($"Security Login Check Error: Wrong PhoneNumber Field");
                return false;
            }

            if (_securityLogin.FullName != result.FullName)
            {
                Console.WriteLine($"Security Login Check Error: Wrong FullName Field");
                return false;
            }

            if (_securityLogin.PrefferredLanguage != result.PrefferredLanguage)
            {
                Console.WriteLine($"Security Login Check Error: Wrong PrefferredLanguage Field");
                return false;
            }

            return true;
        }

        public bool SecurityLoginUpdate()
        {
            _securityLogin.Login = Faker.User.Email();
            _securityLogin.AgreementAccepted = Timestamp.FromDateTimeOffset(Faker.Date.PastWithTime());
            _securityLogin.Created = Timestamp.FromDateTimeOffset(Faker.Date.PastWithTime());
            _securityLogin.EmailAddress = Faker.User.Email();
            _securityLogin.ForceChangePassword = true;
            _securityLogin.FullName = Faker.Name.FullName();
            _securityLogin.IsInactive = true;
            _securityLogin.IsLocked = true;
            _securityLogin.Password = "SoMePassWord@&@";
            _securityLogin.PasswordUpdate = Timestamp.FromDateTimeOffset(Faker.Date.Forward());
            _securityLogin.PhoneNumber = "416-416-9889";
            _securityLogin.PrefferredLanguage = "FR".PadRight(10);


            var securityLoginList = new SecurityLoginList();
            securityLoginList.SecurityLogins.Add(_securityLogin);

            BoolReply result = _securityLoginClient.PutSecurityLogin(securityLoginList);

            if (result.Result == false)
                Console.WriteLine($"Security Login Update Error: {string.Join(", ", result.Errors.Select(e => e.ErrorMessage))}");

            return result.Result;
        }

        #endregion

        #region SecurityLoginsLog Test
        public bool TestSecurityLoginsLog()
        {
            bool result = SecurityLoginsLogAdd();
            if (result == false)
                return false;
            result = SecurityLoginsLogCheck();
            if (result == false)
                return false;
            result = SecurityLoginsLogUpdate();
            if (result == false)
                return false;
            result = SecurityLoginsLogCheck();
            if (result == false)
                return false;

            Console.WriteLine($"SecurityLoginsLog Test Done Successfully.");
            return true;
        }

        private bool SecurityLoginsLogAdd()
        {
            var securityLoginsLogList = new SecurityLoginsLogList();
            securityLoginsLogList.SecurityLoginsLogs.Add(_securityLoginLog);

            BoolReply result = _securityLoginsLogClient.PostSecurityLoginsLog(securityLoginsLogList);

            if (result.Result == false)
                Console.WriteLine($"Security LoginsLog Add Error: {string.Join(", ", result.Errors.Select(e => e.ErrorMessage))}");

            return result.Result;
        }

        private bool SecurityLoginsLogCheck()
        {
            IdRequest request = new IdRequest();
            request.Id = _securityLoginLog.Id;

            SecurityLoginsLogProto result = _securityLoginsLogClient.GetSecurityLoginsLog(request);

            if (result.CalculateSize() <= 0)
            {
                Console.WriteLine($"Security Logins Log Check Error: Null result");
                return false;
            }

            if (_securityLoginLog.Id != result.Id)
            {
                Console.WriteLine($"Security Logins Log Check Error: Wrong Id Field");
                return false;
            }

            if (_securityLoginLog.IsSuccesful != result.IsSuccesful)
            {
                Console.WriteLine($"Security Logins Log Check Error: Wrong IsSuccesful Field");
                return false;
            }

            if (_securityLoginLog.Login != result.Login)
            {
                Console.WriteLine($"Security Logins Log Check Error: Wrong Login Field");
                return false;
            }

            DateTimeOffset offsetlogonDate = result.LogonDate.ToDateTimeOffset();
            Timestamp logonDate = Timestamp.FromDateTimeOffset(offsetlogonDate);

            if (_securityLoginLog.LogonDate.ToDateTime().Date != logonDate.ToDateTime().Date)
            {
                Console.WriteLine($"Security Logins Log Check Error: Wrong LogonDate Field");
                return false;
            }

            if (_securityLoginLog.SourceIp != result.SourceIp)
            {
                Console.WriteLine($"Security Logins Log Check Error: Wrong SourceIp Field");
                return false;
            }

            return true;
        }

        public bool SecurityLoginsLogUpdate()
        {
            _securityLoginLog.IsSuccesful = false;
            _securityLoginLog.LogonDate = Timestamp.FromDateTimeOffset(Faker.Date.PastWithTime());
            _securityLoginLog.SourceIp = Faker.Internet.IPv4().PadRight(15);


            var securityLoginsLogList = new SecurityLoginsLogList();
            securityLoginsLogList.SecurityLoginsLogs.Add(_securityLoginLog);

            BoolReply result = _securityLoginsLogClient.PutSecurityLoginsLog(securityLoginsLogList);

            if (result.Result == false)
                Console.WriteLine($"Security Logins Log Update Error: {string.Join(", ", result.Errors.Select(e => e.ErrorMessage))}");

            return result.Result;
        }

        #endregion
        
        #region ApplicantProfile Test
        public bool TestApplicantProfile()
        {
            bool result = ApplicantProfileAdd();
            if (result == false)
                return false;
            result = ApplicantProfileCheck();
            if (result == false)
                return false;
            result = ApplicantProfileUpdate();
            if (result == false)
                return false;
            result = ApplicantProfileCheck();
            if (result == false)
                return false;

            Console.WriteLine($"ApplicantProfile Test Done Successfully.");
            return true;
        }

        private bool ApplicantProfileAdd()
        {
            var list = new ApplicantProfileList();
            list.ApplicantProfiles.Add(_applicantProfile);

            BoolReply result = _applicantProfileClient.PostApplicantProfile(list);

            if (result.Result == false)
                Console.WriteLine($"Applicant Profile Add Error: {string.Join(", ", result.Errors.Select(e => e.ErrorMessage))}");

            return result.Result;
        }

        private bool ApplicantProfileCheck()
        {
            IdRequest request = new IdRequest();
            request.Id = _applicantProfile.Id;

            ApplicantProfileProto result = _applicantProfileClient.GetApplicantProfile(request);

            if (result.CalculateSize() <= 0)
            {
                Console.WriteLine($"Applicant Profile Check Error: Null result");
                return false;
            }

            if (_applicantProfile.Id != result.Id)
            {
                Console.WriteLine($"Applicant Profile Check Error: Wrong Id Field");
                return false;
            }

            if (_applicantProfile.City != result.City)
            {
                Console.WriteLine($"Applicant Profile Check Error: Wrong City Field");
                return false;
            }

            if (_applicantProfile.Country != result.Country)
            {
                Console.WriteLine($"Applicant Profile Check Error: Wrong Country Field");
                return false;
            }

            if (_applicantProfile.Currency != result.Currency)
            {
                Console.WriteLine($"Applicant Profile Check Error: Wrong Currency Field");
                return false;
            }

            if (_applicantProfile.CurrentRate != result.CurrentRate)
            {
                Console.WriteLine($"Applicant Profile Check Error: Wrong CurrencyRate Field");
                return false;
            }

            if (_applicantProfile.CurrentSalary != result.CurrentSalary)
            {
                Console.WriteLine($"Applicant Profile Check Error: Wrong CurrentSalary Field");
                return false;
            }

            if (_applicantProfile.Login != result.Login)
            {
                Console.WriteLine($"Applicant Profile Check Error: Wrong Login Field");
                return false;
            }

            if (_applicantProfile.PostalCode != result.PostalCode)
            {
                Console.WriteLine($"Applicant Profile Check Error: Wrong PostalCode Field");
                return false;
            }

            if (_applicantProfile.Province != result.Province)
            {
                Console.WriteLine($"Applicant Profile Check Error: Wrong Province Field");
                return false;
            }

            if (_applicantProfile.Street != result.Street)
            {
                Console.WriteLine($"Applicant Profile Check Error: Wrong Street Field");
                return false;
            }

            return true;
        }

        public bool ApplicantProfileUpdate()
        {
            _applicantProfile.City = Faker.Address.CityPrefix();
            _applicantProfile.Currency = "US".PadRight(10);
            _applicantProfile.CurrentRate = 61.25;
            _applicantProfile.CurrentSalary = 77500;
            _applicantProfile.Province = Truncate(Faker.Address.Province(), 10).PadRight(10);
            _applicantProfile.Street = Truncate(Faker.Address.StreetName(), 100);
            _applicantProfile.PostalCode = Truncate(Faker.Address.CanadianZipCode(), 20).PadRight(20);


            var list = new ApplicantProfileList();
            list.ApplicantProfiles.Add(_applicantProfile);

            BoolReply result = _applicantProfileClient.PutApplicantProfile(list);

            if (result.Result == false)
                Console.WriteLine($"Applicant Profile Update Error: {string.Join(", ", result.Errors.Select(e => e.ErrorMessage))}");

            return result.Result;
        }

        #endregion

        #region ApplicantEducation Test
        public bool TestApplicantEducation()
        {
            bool result = ApplicantEducationAdd();
            if (result == false)
                return false;
            result = ApplicantEducationCheck();
            if (result == false)
                return false;
            result = ApplicantEducationUpdate();
            if (result == false)
                return false;
            result = ApplicantEducationCheck();
            if (result == false)
                return false;

            Console.WriteLine($"ApplicantEducation Test Done Successfully.");
            return true;
        }

        private bool ApplicantEducationAdd()
        {
            var list = new EducationList();
            list.ApplicantEducations.Add(_applicantEducation);

            BoolReply result = _applicantEducationClient.PostApplicantEducation(list);

            if (result.Result == false)
                Console.WriteLine($"Applicant Education Add Error: {string.Join(", ", result.Errors.Select(e => e.ErrorMessage))}");

            return result.Result;
        }

        private bool ApplicantEducationCheck()
        {
            IdRequest request = new IdRequest();
            request.Id = _applicantEducation.Id;

            ApplicationEducationProto result = _applicantEducationClient.GetApplicantEducation(request);

            if (result.CalculateSize() <= 0)
            {
                Console.WriteLine($"Applicant Education Check Error: Null result");
                return false;
            }

            if (_applicantEducation.Id != result.Id)
            {
                Console.WriteLine($"Applicant Education Check Error: Wrong Id Field");
                return false;
            }

            if (_applicantEducation.Applicant != result.Applicant)
            {
                Console.WriteLine($"Applicant Education Check Error: Wrong Applicant Field");
                return false;
            }

            if (_applicantEducation.CompletionDate.ToDateTimeOffset().Date != result.CompletionDate.ToDateTimeOffset().Date)
            {
                Console.WriteLine($"Applicant Education Check Error: Wrong CompletionDate Field");
                return false;
            }

            if (_applicantEducation.CompletionPercent != result.CompletionPercent)
            {
                Console.WriteLine($"Applicant Education Check Error: Wrong CompletionPercent Field");
                return false;
            }

            if (_applicantEducation.Major != result.Major)
            {
                Console.WriteLine($"Applicant Education Check Error: Wrong Major Field");
                return false;
            }

            if (_applicantEducation.StartDate.ToDateTimeOffset().Date != result.StartDate.ToDateTimeOffset().Date)
            {
                Console.WriteLine($"Applicant Education Check Error: Wrong StartDate Field");
                return false;
            }

            return true;
        }

        public bool ApplicantEducationUpdate()
        {
            _applicantEducation.Major = Faker.Education.Major();
            _applicantEducation.CertificateDiploma = Faker.Education.Major();
            _applicantEducation.StartDate = Timestamp.FromDateTimeOffset(Faker.Date.Past(3));
            _applicantEducation.CompletionDate = Timestamp.FromDateTimeOffset(Faker.Date.Forward(1));
            _applicantEducation.CompletionPercent = Google.Protobuf.ByteString.CopyFrom(new byte[] { (byte)Faker.Number.RandomNumber(1) });


            var list = new EducationList();
            list.ApplicantEducations.Add(_applicantEducation);

            BoolReply result = _applicantEducationClient.PutApplicantEducation(list);

            if (result.Result == false)
                Console.WriteLine($"Applicant Education Update Error: {string.Join(", ", result.Errors.Select(e => e.ErrorMessage))}");

            return result.Result;
        }

        #endregion

        #region ApplicantJobApplication Test
        public bool TestApplicantJobApplication()
        {
            bool result = ApplicantJobApplicationAdd();
            if (result == false)
                return false;
            result = ApplicantJobApplicationCheck();
            if (result == false)
                return false;
            result = ApplicantJobApplicationUpdate();
            if (result == false)
                return false;
            result = ApplicantJobApplicationCheck();
            if (result == false)
                return false;

            Console.WriteLine($"ApplicantJobApplication Test Done Successfully.");
            return true;
        }

        private bool ApplicantJobApplicationAdd()
        {
            var list = new ApplicantJobApplicationList();
            list.ApplicantJobApplications.Add(_applicantJobApplication);

            BoolReply result = _applicantJobApplicationClient.PostApplicantJobApplication(list);

            if (result.Result == false)
                Console.WriteLine($"Applicant JobApplication Add Error: {string.Join(", ", result.Errors.Select(e => e.ErrorMessage))}");

            return result.Result;
        }

        private bool ApplicantJobApplicationCheck()
        {
            IdRequest request = new IdRequest();
            request.Id = _applicantJobApplication.Id;

            ApplicantJobApplicationProto result = _applicantJobApplicationClient.GetApplicantJobApplication(request);

            if (result.CalculateSize() <= 0)
            {
                Console.WriteLine($"Applicant Job Application Check Error: Null result");
                return false;
            }

            if (_applicantJobApplication.Id != result.Id)
            {
                Console.WriteLine($"Applicant Job Application Check Error: Wrong Id Field");
                return false;
            }

            if (_applicantJobApplication.Applicant != result.Applicant)
            {
                Console.WriteLine($"Applicant Job Application Check Error: Wrong Applicant Field");
                return false;
            }

            if (_applicantJobApplication.ApplicationDate.ToDateTimeOffset().Date != result.ApplicationDate.ToDateTimeOffset().Date)
            {
                Console.WriteLine($"Applicant Job Application Check Error: Wrong ApplicationDate Field");
                return false;
            }

            if (_applicantJobApplication.Job != result.Job)
            {
                Console.WriteLine($"Applicant Job Application Check Error: Wrong Job Field");
                return false;
            }

            return true;
        }

        public bool ApplicantJobApplicationUpdate()
        {
            _applicantJobApplication.ApplicationDate = Timestamp.FromDateTimeOffset(Faker.Date.Recent());

            var list = new ApplicantJobApplicationList();
            list.ApplicantJobApplications.Add(_applicantJobApplication);

            BoolReply result = _applicantJobApplicationClient.PutApplicantJobApplication(list);

            if (result.Result == false)
                Console.WriteLine($"Applicant JobApplication Update Error: {string.Join(", ", result.Errors.Select(e => e.ErrorMessage))}");

            return result.Result;
        }

        #endregion


        #region Cleanup

        private bool ApplicantJobApplicationRemove()
        {
            var list = new ApplicantJobApplicationList();
            list.ApplicantJobApplications.Add(_applicantJobApplication);
            var result = _applicantJobApplicationClient.DeleteApplicantJobApplication(list);

            var idRequest = new IdRequest { Id = _applicantJobApplication.Id };
            var response = _applicantJobApplicationClient.GetApplicantJobApplication(idRequest);

            if (response.CalculateSize() > 0)
            {
                Console.WriteLine("Delete of ApplicantJobApplication not worked");
                return false;
            }

            return true;
        }

        private bool ApplicantEducationRemove()
        {
            var list = new EducationList();
            list.ApplicantEducations.Add(_applicantEducation);
            var result = _applicantEducationClient.DeleteApplicantEducation(list);

            var idRequest = new IdRequest { Id = _applicantEducation.Id };
            var response = _applicantEducationClient.GetApplicantEducation(idRequest);

            if (response.CalculateSize() > 0)
            {
                Console.WriteLine("Delete of ApplicantEducation not worked");
                return false;
            }

            return true;
        }

        private bool ApplicantProfileRemove()
        {
            var list = new ApplicantProfileList();
            list.ApplicantProfiles.Add(_applicantProfile);
            var result = _applicantProfileClient.DeleteApplicantProfile(list);

            var idRequest = new IdRequest { Id = _applicantProfile.Id };
            var response = _applicantProfileClient.GetApplicantProfile(idRequest);

            if (response.CalculateSize() > 0)
            {
                Console.WriteLine("Delete of ApplicantProfile not worked");
                return false;
            }

            return true;
        }

        private bool SecurityLoginLogRemove()
        {
            var list = new SecurityLoginsLogList();
            list.SecurityLoginsLogs.Add(_securityLoginLog);
            var result = _securityLoginsLogClient.DeleteSecurityLoginsLog(list);

            var idRequest = new IdRequest { Id = _securityLoginLog.Id };
            var response = _securityLoginsLogClient.GetSecurityLoginsLog(idRequest);

            if (response.CalculateSize() > 0)
            {
                Console.WriteLine("Delete of SecurityLoginsLog not worked");
                return false;
            }

            return true;
        }

        private bool SecurityLoginRemove()
        {
            var list = new SecurityLoginList();
            list.SecurityLogins.Add(_securityLogin);
            var result = _securityLoginClient.DeleteSecurityLogin(list);

            var idRequest = new IdRequest { Id = _securityLogin.Id };
            var response = _securityLoginClient.GetSecurityLogin(idRequest);

            if (response.CalculateSize() > 0)
            {
                Console.WriteLine("Delete of SecurityLogin not worked");
                return false;
            }

            return true;
        }

        private bool CompanyJobEducationRemove()
        {
            var list = new CompanyJobEducationList();
            list.CompanyJobEducations.Add(_companyJobEducation);
            var result = _companyJobEducationClient.DeleteCompanyJobEducation(list);

            var idRequest = new IdRequest { Id = _companyJobEducation.Id };
            var response = _companyJobEducationClient.GetCompanyJobEducation(idRequest);

            if (response.CalculateSize() > 0)
            {
                Console.WriteLine("Delete of CompanyJobEducation not worked");
                return false;
            }

            return true;
        }

        private bool CompanyJobRemove()
        {
            var list = new CompanyJobList();
            list.CompanyJobs.Add(_companyJob);
            var result = _companyJobClient.DeleteCompanyJob(list);

            var idRequest = new IdRequest { Id = _companyJob.Id };
            var response = _companyJobClient.GetCompanyJob(idRequest);

            if (response.CalculateSize() > 0)
            {
                Console.WriteLine("Delete of CompanyJob not worked");
                return false;
            }

            return true;
        }

        private bool CompanyDescriptionRemove()
        {
            var list = new CompanyDescriptionList();
            list.CompanyDescriptions.Add(_companyDescription);
            var result = _companyDescriptionClient.DeleteCompanyDescription(list);

            var idRequest = new IdRequest { Id = _companyDescription.Id };
            var response = _companyDescriptionClient.GetCompanyDescription(idRequest);

            if (response.CalculateSize() > 0)
            {
                Console.WriteLine("Delete of CompanyDescription not worked");
                return false;
            }

            return true;
        }

        private bool SystemLanguageCodeRemove()
        {
            var list = new SystemLanguageCodeList();
            list.SystemLanguageCodes.Add(_systemLangCode);
            var result = _systemLanguageCodeClient.DeleteSystemLanguageCode(list);

            var idRequest = new IdRequest { Id = _systemLangCode.LanguageId };
            var response = _systemLanguageCodeClient.GetSystemLanguageCode(idRequest);

            if (response.CalculateSize() > 0)
            {
                Console.WriteLine("Delete of SystemLanguageCode not worked");
                return false;
            }

            return true;
        }

        #endregion




    }
}
