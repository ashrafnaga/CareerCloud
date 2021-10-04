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
    public class SecurityLoginsLogService : SecurityLoginsLogProvider.SecurityLoginsLogProviderBase
    {
        SecurityLoginsLogLogic _logic;

        public SecurityLoginsLogService()
        {
            var repository = new EFGenericRepository<SecurityLoginsLogPoco>();
            _logic = new SecurityLoginsLogLogic(repository);
        }

        public override Task<BoolReply> DeleteSecurityLoginsLog(SecurityLoginsLogList request, ServerCallContext context)
        {
            var items = new List<SecurityLoginsLogPoco>();
            foreach (var item in request.SecurityLoginsLogs)
            {
                items.Add(new SecurityLoginsLogPoco
                {
                    Id = new Guid(item.Id),
                    IsSuccesful = item.IsSuccesful,
                    LogonDate = item.LogonDate.ToDateTime(),
                    SourceIP = item.SourceIp,
                    Login = new Guid(item.Login)
                });
            }

            _logic.Delete(items.ToArray());

            var result = new BoolReply { Result = true };

            var task = Task.FromResult(result);

            return task;
        }

        public override Task<SecurityLoginsLogProto> GetSecurityLoginsLog(IdRequest request, ServerCallContext context)
        {
            var item = _logic.Get(new Guid(request.Id));

            SecurityLoginsLogProto result = new SecurityLoginsLogProto();
            if (item != null)
            {
                result = new SecurityLoginsLogProto
                {
                    Id = item.Id.ToString(),
                    IsSuccesful = item.IsSuccesful,
                    LogonDate = Timestamp.FromDateTimeOffset(item.LogonDate.ToLocalTime()),
                    SourceIp = item.SourceIP,
                    Login = item.Login.ToString()
                };
            }

            var task = Task.FromResult(result);

            return task;
        }

        public override Task<SecurityLoginsLogList> GetSecurityLoginsLogs(Empty request, ServerCallContext context)
        {
            var items = _logic.GetAll();

            var result = new SecurityLoginsLogList();
            foreach (var item in items)
            {
                result.SecurityLoginsLogs.Add(new SecurityLoginsLogProto
                {
                    Id = item.Id.ToString(),
                    IsSuccesful = item.IsSuccesful,
                    LogonDate = Timestamp.FromDateTimeOffset(item.LogonDate.ToLocalTime()),
                    SourceIp = item.SourceIP,
                    Login = item.Login.ToString()
                });
            }

            var task = Task.FromResult(result);

            return task;
        }

        public override Task<BoolReply> PostSecurityLoginsLog(SecurityLoginsLogList request, ServerCallContext context)
        {
            var items = new List<SecurityLoginsLogPoco>();
            foreach (var item in request.SecurityLoginsLogs)
            {
                items.Add(new SecurityLoginsLogPoco
                {
                    Id = new Guid(item.Id),
                    IsSuccesful = item.IsSuccesful,
                    LogonDate = item.LogonDate.ToDateTime(),
                    SourceIP = item.SourceIp,
                    Login = new Guid(item.Login)
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

        public override Task<BoolReply> PutSecurityLoginsLog(SecurityLoginsLogList request, ServerCallContext context)
        {
            var items = new List<SecurityLoginsLogPoco>();
            foreach (var item in request.SecurityLoginsLogs)
            {
                items.Add(new SecurityLoginsLogPoco
                {
                    Id = new Guid(item.Id),
                    IsSuccesful = item.IsSuccesful,
                    LogonDate = item.LogonDate.ToDateTime(),
                    SourceIP = item.SourceIp,
                    Login = new Guid(item.Login)
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
