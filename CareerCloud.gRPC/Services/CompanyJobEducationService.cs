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
    public class CompanyJobEducationService : CompanyJobEducationProvider.CompanyJobEducationProviderBase
    {
        CompanyJobEducationLogic _logic;

        public CompanyJobEducationService()
        {
            var repository = new EFGenericRepository<CompanyJobEducationPoco>();
            _logic = new CompanyJobEducationLogic(repository);
        }

        public override Task<BoolReply> DeleteCompanyJobEducation(CompanyJobEducationList request, ServerCallContext context)
        {
            var items = new List<CompanyJobEducationPoco>();
            foreach (var item in request.CompanyJobEducations)
            {
                var mainItem = _logic.Get(new Guid(item.Id));

                mainItem.Importance = (short)item.Importance;
                mainItem.Job = new Guid(item.Job);
                mainItem.Major = item.Major;
                mainItem.TimeStamp = item.TimeStamp.ToByteArray();

                items.Add(mainItem);
            }

            _logic.Delete(items.ToArray());

            var result = new BoolReply { Result = true };

            var task = Task.FromResult(result);

            return task;
        }

        public override Task<CompanyJobEducationProto> GetCompanyJobEducation(IdRequest request, ServerCallContext context)
        {
            var item = _logic.Get(new Guid(request.Id));

            CompanyJobEducationProto result = new CompanyJobEducationProto();
            if (item != null)
            {
                result = new CompanyJobEducationProto
                {
                    Id = item.Id.ToString(),
                    Importance = item.Importance,
                    Job = item.Job.ToString(),
                    Major = item.Major,
                    TimeStamp = Google.Protobuf.ByteString.CopyFrom(item.TimeStamp)
                };
            }

            var task = Task.FromResult(result);

            return task;
        }

        public override Task<CompanyJobEducationList> GetCompanyJobEducations(Empty request, ServerCallContext context)
        {
            var items = _logic.GetAll();

            var result = new CompanyJobEducationList();
            foreach (var item in items)
            {
                result.CompanyJobEducations.Add(new CompanyJobEducationProto
                {
                    Id = item.Id.ToString(),
                    Importance = item.Importance,
                    Job = item.Job.ToString(),
                    Major = item.Major,
                    TimeStamp = Google.Protobuf.ByteString.CopyFrom(item.TimeStamp)
                });
            }

            var task = Task.FromResult(result);

            return task;
        }

        public override Task<BoolReply> PostCompanyJobEducation(CompanyJobEducationList request, ServerCallContext context)
        {
            var items = new List<CompanyJobEducationPoco>();
            foreach (var item in request.CompanyJobEducations)
            {
                items.Add(new CompanyJobEducationPoco
                {
                    Id = new Guid(item.Id),
                    Importance = (short)item.Importance,
                    Job = new Guid(item.Job),
                    Major = item.Major,
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

        public override Task<BoolReply> PutCompanyJobEducation(CompanyJobEducationList request, ServerCallContext context)
        {
            try
            {
                var items = new List<CompanyJobEducationPoco>();
                foreach (var item in request.CompanyJobEducations)
                {
                    var mainItem = _logic.Get(new Guid(item.Id));

                    mainItem.Importance = (short)item.Importance;
                    mainItem.Job = new Guid(item.Job);
                    mainItem.Major = item.Major;
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
