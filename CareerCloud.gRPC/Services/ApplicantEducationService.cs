using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.gRPC.Common;
using CareerCloud.Pocos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CareerCloud.gRPC.Services
{
    public class ApplicantEducationService : ApplicantEducationProvider.ApplicantEducationProviderBase
    {
        ApplicantEducationLogic _applicationEducationLogic;

        public ApplicantEducationService()
        {
            var repository = new EFGenericRepository<ApplicantEducationPoco>();
            _applicationEducationLogic = new ApplicantEducationLogic(repository);
        }

        public override Task<BoolReply> DeleteApplicantEducation(EducationList request, ServerCallContext context)
        {
            var items = new List<ApplicantEducationPoco>();
            foreach (var item in request.ApplicantEducations)
            {
                var mainItem = _applicationEducationLogic.Get(new Guid(item.Id));

                mainItem.Applicant = new Guid(item.Applicant);
                mainItem.CertificateDiploma = item.CertificateDiploma;
                mainItem.CompletionDate = item.CompletionDate.ToDateTime();
                mainItem.StartDate = item.StartDate.ToDateTime();
                mainItem.CompletionPercent = item.CompletionPercent.ToByteArray().FirstOrDefault();
                mainItem.Major = item.Major;
                mainItem.TimeStamp = item.TimeStamp.ToByteArray();

                items.Add(mainItem);
            }

            _applicationEducationLogic.Delete(items.ToArray());

            var result = new BoolReply { Result = true };

            var task = Task.FromResult(result);

            return task;
        }

        public override Task<ApplicationEducationProto> GetApplicantEducation(IdRequest request, ServerCallContext context)
        {
            ApplicantEducationPoco item = _applicationEducationLogic.Get(new Guid(request.Id));

            ApplicationEducationProto result = new ApplicationEducationProto();
            if (item != null)
            {

                result = new ApplicationEducationProto
                {
                    Id = item.Id.ToString(),
                    Applicant = item.Applicant.ToString(),
                    CertificateDiploma = item.CertificateDiploma,
                    CompletionDate = item.CompletionDate == null ? null : Timestamp.FromDateTimeOffset(item.CompletionDate.Value.ToLocalTime()),
                    StartDate = item.StartDate == null ? null : Timestamp.FromDateTimeOffset(item.StartDate.Value.ToLocalTime()),
                    CompletionPercent = item.CompletionPercent == null ? null : Google.Protobuf.ByteString.CopyFrom(new byte[] { item.CompletionPercent.Value }),
                    Major = item.Major,
                    TimeStamp = Google.Protobuf.ByteString.CopyFrom(item.TimeStamp)
                };
            }

            var task = Task.FromResult(result);

            return task;
        }

        public override Task<EducationList> GetApplicantEducations(Empty request, ServerCallContext context)
        {
            var items = _applicationEducationLogic.GetAll();

            var result = new EducationList();
            foreach (var item in items)
            {
                result.ApplicantEducations.Add(new ApplicationEducationProto
                {
                    Id = item.Id.ToString(),
                    Applicant = item.Applicant.ToString(),
                    CertificateDiploma = item.CertificateDiploma,
                    CompletionDate = item.CompletionDate == null ? null : Timestamp.FromDateTimeOffset(item.CompletionDate.Value.ToLocalTime()),
                    StartDate = item.StartDate == null ? null : Timestamp.FromDateTimeOffset(item.StartDate.Value.ToLocalTime()),
                    CompletionPercent = item.CompletionPercent == null ? null : Google.Protobuf.ByteString.CopyFrom(new byte[] { item.CompletionPercent.Value }),
                    Major = item.Major,
                    TimeStamp = Google.Protobuf.ByteString.CopyFrom(item.TimeStamp)
                });
            }

            var task = Task.FromResult(result);

            return task;
        }
        
        public override Task<BoolReply> PostApplicantEducation(EducationList request, ServerCallContext context)
        {
            var items = new List<ApplicantEducationPoco>();
            foreach (var item in request.ApplicantEducations)
            {
                items.Add(new ApplicantEducationPoco
                {
                    Id = new Guid(item.Id),
                    Applicant = new Guid(item.Applicant),
                    CertificateDiploma = item.CertificateDiploma,
                    CompletionDate = item.CompletionDate.ToDateTime(),
                    StartDate = item.StartDate.ToDateTime(),
                    CompletionPercent = item.CompletionPercent.ToByteArray().FirstOrDefault(),
                    Major = item.Major,
                    TimeStamp = item.TimeStamp.ToByteArray()
                });
            }

            
            try
            {
                _applicationEducationLogic.Add(items.ToArray());

                var result = new BoolReply { Result = true };

                var task = Task.FromResult(result);

                return task;
            }
            catch (AggregateException ex)
            {
                BoolReply result = new BoolReply();
                result.Result = false;
                foreach (var exeption in ex.InnerExceptions)
                {

                    int code = ((ValidationException)exeption).Code;

                    result.Errors.Add(new ErrorReply { ErrorCode = code, ErrorMessage = exeption.Message });
                }
                var task = Task.FromResult(result);

                return task;
            }
            catch (Exception ex)
            {
                BoolReply result = new BoolReply();
                result.Result = false;
                result.Errors.Add(new ErrorReply { ErrorCode = 500, ErrorMessage = $"General Error: {ex.Message}" });

                var task = Task.FromResult(result);

                return task;
            }
            
        }

        public override Task<BoolReply> PutApplicantEducation(EducationList request, ServerCallContext context)
        {
            try
            {
                var items = new List<ApplicantEducationPoco>();
                foreach (var item in request.ApplicantEducations)
                {
                    var mainItem = _applicationEducationLogic.Get(new Guid(item.Id));

                    mainItem.Applicant = new Guid(item.Applicant);
                    mainItem.CertificateDiploma = item.CertificateDiploma;
                    mainItem.CompletionDate = item.CompletionDate.ToDateTime();
                    mainItem.StartDate = item.StartDate.ToDateTime();
                    mainItem.CompletionPercent = item.CompletionPercent.ToByteArray().FirstOrDefault();
                    mainItem.Major = item.Major;
                    mainItem.TimeStamp = item.TimeStamp.ToByteArray();

                    items.Add(mainItem);
                }

                _applicationEducationLogic.Update(items.ToArray());

                var result = new BoolReply { Result = true };

                var task = Task.FromResult(result);

                return task;
            }
            catch (AggregateException ex)
            {
                BoolReply result = new BoolReply();
                result.Result = false;
                foreach (var exeption in ex.InnerExceptions)
                {

                    int code = ((ValidationException)exeption).Code;

                    result.Errors.Add(new ErrorReply { ErrorCode = code, ErrorMessage = exeption.Message });
                }
                var task = Task.FromResult(result);

                return task;
            }
            catch (Exception ex)
            {
                BoolReply result = new BoolReply();
                result.Result = false;
                result.Errors.Add(new ErrorReply { ErrorCode = 500, ErrorMessage = $"General Error: {ex.Message}" });

                var task = Task.FromResult(result);

                return task;
            }
        }
        
    }
    
}
