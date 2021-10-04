using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.gRPC.Common;
using CareerCloud.Pocos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CareerCloud.gRPC.Services
{
    public class ApplicantJobApplicationService : ApplicantJobApplicationProvider.ApplicantJobApplicationProviderBase
    {
        ApplicantJobApplicationLogic _logic;

        public ApplicantJobApplicationService()
        {
            var repository = new EFGenericRepository<ApplicantJobApplicationPoco>();
            _logic = new ApplicantJobApplicationLogic(repository);
        }

        public override Task<BoolReply> DeleteApplicantJobApplication(ApplicantJobApplicationList request, ServerCallContext context)
        {
            var items = new List<ApplicantJobApplicationPoco>();
            foreach (var item in request.ApplicantJobApplications)
            {
                var mainItem = _logic.Get(new Guid(item.Id));

                mainItem.Applicant = new Guid(item.Applicant);
                mainItem.Job = new Guid(item.Job);
                mainItem.ApplicationDate = item.ApplicationDate.ToDateTime();
                mainItem.TimeStamp = item.TimeStamp.ToByteArray();

                items.Add(mainItem);
            }

            _logic.Delete(items.ToArray());

            var result = new BoolReply { Result = true };

            var task = Task.FromResult(result);

            return task;
        }

        public override Task<ApplicantJobApplicationProto> GetApplicantJobApplication(IdRequest request, ServerCallContext context)
        {
            ApplicantJobApplicationPoco item = _logic.Get(new Guid(request.Id));

            ApplicantJobApplicationProto result = new ApplicantJobApplicationProto();
            if (item != null)
            {

                result = new ApplicantJobApplicationProto
                {
                    Id = item.Id.ToString(),
                    Applicant = item.Applicant.ToString(),
                    Job = item.Job.ToString(),
                    ApplicationDate = Timestamp.FromDateTimeOffset(item.ApplicationDate.ToLocalTime()),
                    TimeStamp = Google.Protobuf.ByteString.CopyFrom(item.TimeStamp)
                };
            }

            var task = Task.FromResult(result);

            return task;
        }

        public override Task<ApplicantJobApplicationList> GetApplicantJobApplications(Empty request, ServerCallContext context)
        {
            var items = _logic.GetAll();

            var result = new ApplicantJobApplicationList();
            foreach (var item in items)
            {
                result.ApplicantJobApplications.Add(new ApplicantJobApplicationProto
                {
                    Id = item.Id.ToString(),
                    Applicant = item.Applicant.ToString(),
                    Job = item.Job.ToString(),
                    ApplicationDate = Timestamp.FromDateTimeOffset(item.ApplicationDate.ToLocalTime()),
                    TimeStamp = Google.Protobuf.ByteString.CopyFrom(item.TimeStamp)
                });
            }

            var task = Task.FromResult(result);

            return task;
        }

        public override Task<BoolReply> PostApplicantJobApplication(ApplicantJobApplicationList request, ServerCallContext context)
        {
            var items = new List<ApplicantJobApplicationPoco>();
            foreach (var item in request.ApplicantJobApplications)
            {
                items.Add(new ApplicantJobApplicationPoco
                {
                    Id = new Guid(item.Id),
                    Applicant = new Guid(item.Applicant),
                    Job = new Guid(item.Job),
                    ApplicationDate = item.ApplicationDate.ToDateTime(),
                    TimeStamp = item.TimeStamp.ToByteArray()
                });
            }

            try
            {
                _logic.Add(items.ToArray());

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

        public override Task<BoolReply> PutApplicantJobApplication(ApplicantJobApplicationList request, ServerCallContext context)
        {
            try
            {
                var items = new List<ApplicantJobApplicationPoco>();
                foreach (var item in request.ApplicantJobApplications)
                {
                    var mainItem = _logic.Get(new Guid(item.Id));

                    mainItem.Applicant = new Guid(item.Applicant);
                    mainItem.Job = new Guid(item.Job);
                    mainItem.ApplicationDate = item.ApplicationDate.ToDateTime();
                    mainItem.TimeStamp = item.TimeStamp.ToByteArray();

                    items.Add(mainItem);
                }

                _logic.Update(items.ToArray());

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
