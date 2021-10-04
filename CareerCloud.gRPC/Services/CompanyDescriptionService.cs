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
    public class CompanyDescriptionService : CompanyDescriptionProvider.CompanyDescriptionProviderBase
    {
        CompanyDescriptionLogic _logic;

        public CompanyDescriptionService()
        {
            var repository = new EFGenericRepository<CompanyDescriptionPoco>();
            _logic = new CompanyDescriptionLogic(repository);
        }

        public override Task<BoolReply> DeleteCompanyDescription(CompanyDescriptionList request, ServerCallContext context)
        {
            var items = new List<CompanyDescriptionPoco>();
            foreach (var item in request.CompanyDescriptions)
            {
                var mainItem = _logic.Get(new Guid(item.Id));
                mainItem.Company = new Guid(item.Company);
                mainItem.CompanyDescription = item.CompanyDescription;
                mainItem.CompanyName = item.CompanyName;
                mainItem.LanguageId = item.LanguageId;
                mainItem.TimeStamp = item.TimeStamp.ToByteArray();

                items.Add(mainItem);
            }

            _logic.Delete(items.ToArray());

            var result = new BoolReply { Result = true };

            var task = Task.FromResult(result);

            return task;
        }

        public override Task<CompanyDescriptionProto> GetCompanyDescription(IdRequest request, ServerCallContext context)
        {
            var item = _logic.Get(new Guid(request.Id));

            CompanyDescriptionProto result = new CompanyDescriptionProto();
            if (item != null)
            {
                result = new CompanyDescriptionProto
                {
                    Id = item.Id.ToString(),
                    Company = item.Company.ToString(),
                    CompanyDescription = item.CompanyDescription,
                    CompanyName = item.CompanyName,
                    LanguageId = item.LanguageId,
                    TimeStamp = Google.Protobuf.ByteString.CopyFrom(item.TimeStamp)
                };
            }

            var task = Task.FromResult(result);

            return task;
        }

        public override Task<CompanyDescriptionList> GetCompanyDescriptions(Empty request, ServerCallContext context)
        {
            var items = _logic.GetAll();

            var result = new CompanyDescriptionList();
            foreach (var item in items)
            {
                result.CompanyDescriptions.Add(new CompanyDescriptionProto
                {
                    Id = item.Id.ToString(),
                    Company = item.Company.ToString(),
                    CompanyDescription = item.CompanyDescription,
                    CompanyName = item.CompanyName,
                    LanguageId = item.LanguageId,
                    TimeStamp = Google.Protobuf.ByteString.CopyFrom(item.TimeStamp)
                });
            }

            var task = Task.FromResult(result);

            return task;
        }

        public override Task<BoolReply> PostCompanyDescription(CompanyDescriptionList request, ServerCallContext context)
        {
            var items = new List<CompanyDescriptionPoco>();
            foreach (var item in request.CompanyDescriptions)
            {
                items.Add(new CompanyDescriptionPoco
                {
                    Id = new Guid(item.Id),
                    Company = new Guid(item.Company),
                    CompanyDescription = item.CompanyDescription,
                    CompanyName = item.CompanyName,
                    LanguageId = item.LanguageId,
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

        public override Task<BoolReply> PutCompanyDescription(CompanyDescriptionList request, ServerCallContext context)
        {
            try
            {
                var items = new List<CompanyDescriptionPoco>();
                foreach (var item in request.CompanyDescriptions)
                {
                    var mainItem = _logic.Get(new Guid(item.Id));
                    mainItem.Company = new Guid(item.Company);
                    mainItem.CompanyDescription = item.CompanyDescription;
                    mainItem.CompanyName = item.CompanyName;
                    mainItem.LanguageId = item.LanguageId;
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
