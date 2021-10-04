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
    public class CompanyJobService : CompanyJobProvider.CompanyJobProviderBase
    {
        CompanyJobLogic _logic;

        public CompanyJobService()
        {
            var repository = new EFGenericRepository<CompanyJobPoco>();
            _logic = new CompanyJobLogic(repository);
        }

        public override Task<BoolReply> DeleteCompanyJob(CompanyJobList request, ServerCallContext context)
        {
            var items = new List<CompanyJobPoco>();
            foreach (var item in request.CompanyJobs)
            {
                var mainItem = _logic.Get(new Guid(item.Id));

                mainItem.Company = new Guid(item.Company);
                mainItem.IsCompanyHidden = item.IsCompanyHidden;
                mainItem.IsInactive = item.IsInactive;
                mainItem.ProfileCreated = item.ProfileCreated.ToDateTime();
                mainItem.TimeStamp = item.TimeStamp.ToByteArray();

                items.Add(mainItem);
            }

            _logic.Delete(items.ToArray());

            var result = new BoolReply { Result = true };

            var task = Task.FromResult(result);

            return task;
        }

        public override Task<CompanyJobProto> GetCompanyJob(IdRequest request, ServerCallContext context)
        {
            var item = _logic.Get(new Guid(request.Id));
            CompanyJobProto result = new CompanyJobProto();
            if (item != null)
            {
                result = new CompanyJobProto
                {
                    Id = item.Id.ToString(),
                    Company = item.Company.ToString(),
                    IsCompanyHidden = item.IsCompanyHidden,
                    IsInactive = item.IsInactive,
                    ProfileCreated = Timestamp.FromDateTimeOffset(item.ProfileCreated.ToLocalTime()),
                    TimeStamp = Google.Protobuf.ByteString.CopyFrom(item.TimeStamp)
                };
            }

            var task = Task.FromResult(result);

            return task;
        }

        public override Task<CompanyJobList> GetCompanyJobs(Empty request, ServerCallContext context)
        {
            var items = _logic.GetAll();

            var result = new CompanyJobList();
            foreach (var item in items)
            {
                result.CompanyJobs.Add(new CompanyJobProto
                {
                    Id = item.Id.ToString(),
                    Company = item.Company.ToString(),
                    IsCompanyHidden = item.IsCompanyHidden,
                    IsInactive = item.IsInactive,
                    ProfileCreated = Timestamp.FromDateTimeOffset(item.ProfileCreated.ToLocalTime()),
                    TimeStamp = Google.Protobuf.ByteString.CopyFrom(item.TimeStamp)
                });
            }

            var task = Task.FromResult(result);

            return task;
        }

        public override Task<BoolReply> PostCompanyJob(CompanyJobList request, ServerCallContext context)
        {
            var items = new List<CompanyJobPoco>();
            foreach (var item in request.CompanyJobs)
            {
                items.Add(new CompanyJobPoco
                {
                    Id = new Guid(item.Id),
                    Company = new Guid(item.Company),
                    IsCompanyHidden = item.IsCompanyHidden,
                    IsInactive = item.IsInactive,
                    ProfileCreated = item.ProfileCreated.ToDateTime(),
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

        public override Task<BoolReply> PutCompanyJob(CompanyJobList request, ServerCallContext context)
        {
            try
            {
                var items = new List<CompanyJobPoco>();
                foreach (var item in request.CompanyJobs)
                {
                    var mainItem = _logic.Get(new Guid(item.Id));

                    mainItem.Company = new Guid(item.Company);
                    mainItem.IsCompanyHidden = item.IsCompanyHidden;
                    mainItem.IsInactive = item.IsInactive;
                    mainItem.ProfileCreated = item.ProfileCreated.ToDateTime();
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
