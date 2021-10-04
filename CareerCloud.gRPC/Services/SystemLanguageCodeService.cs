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
    public class SystemLanguageCodeService : SystemLanguageCodeProvider.SystemLanguageCodeProviderBase
    {
        SystemLanguageCodeLogic _logic;

        public SystemLanguageCodeService()
        {
            var repository = new EFGenericRepository<SystemLanguageCodePoco>();
            _logic = new SystemLanguageCodeLogic(repository);
        }

        public override Task<BoolReply> DeleteSystemLanguageCode(SystemLanguageCodeList request, ServerCallContext context)
        {
            var items = new List<SystemLanguageCodePoco>();
            foreach (var item in request.SystemLanguageCodes)
            {
                items.Add(new SystemLanguageCodePoco
                {
                    LanguageID = item.LanguageId,
                    Name = item.Name,
                    NativeName = item.NativeName
                });
            }

            _logic.Delete(items.ToArray());

            var result = new BoolReply { Result = true };

            var task = Task.FromResult(result);

            return task;
        }

        public override Task<SystemLanguageCodeProto> GetSystemLanguageCode(IdRequest request, ServerCallContext context)
        {
            var item = _logic.Get(request.Id);

            SystemLanguageCodeProto result = new SystemLanguageCodeProto();
            if (item != null)
            {
                result = new SystemLanguageCodeProto
                {
                    LanguageId = item.LanguageID,
                    Name = item.Name,
                    NativeName = item.NativeName
                };
            }

            var task = Task.FromResult(result);

            return task;
        }

        public override Task<SystemLanguageCodeList> GetSystemLanguageCodes(Empty request, ServerCallContext context)
        {
            var items = _logic.GetAll();

            var result = new SystemLanguageCodeList();
            foreach (var item in items)
            {
                result.SystemLanguageCodes.Add(new SystemLanguageCodeProto
                {
                    LanguageId = item.LanguageID,
                    Name = item.Name,
                    NativeName = item.NativeName
                });
            }

            var task = Task.FromResult(result);

            return task;
        }

        public override Task<BoolReply> PostSystemLanguageCode(SystemLanguageCodeList request, ServerCallContext context)
        {
            var items = new List<SystemLanguageCodePoco>();
            foreach (var item in request.SystemLanguageCodes)
            {
                items.Add(new SystemLanguageCodePoco
                {
                    LanguageID = item.LanguageId,
                    Name = item.Name,
                    NativeName = item.NativeName
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

        public override Task<BoolReply> PutSystemLanguageCode(SystemLanguageCodeList request, ServerCallContext context)
        {
            var items = new List<SystemLanguageCodePoco>();
            foreach (var item in request.SystemLanguageCodes)
            {
                items.Add(new SystemLanguageCodePoco
                {
                    LanguageID = item.LanguageId,
                    Name = item.Name,
                    NativeName = item.NativeName
                });
            }

            try
            {
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
