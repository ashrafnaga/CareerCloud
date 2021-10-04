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
    public class SecurityLoginService : SecurityLoginProvider.SecurityLoginProviderBase
    {
        SecurityLoginLogic _logic;

        public SecurityLoginService()
        {
            var repository = new EFGenericRepository<SecurityLoginPoco>();
            _logic = new SecurityLoginLogic(repository);
        }

        public override Task<BoolReply> DeleteSecurityLogin(SecurityLoginList request, ServerCallContext context)
        {
            var items = new List<SecurityLoginPoco>();
            foreach (var item in request.SecurityLogins)
            {
                var mainItem = _logic.Get(new Guid(item.Id));

                mainItem.AgreementAccepted = item.AgreementAccepted.ToDateTime();
                mainItem.Created = item.Created.ToDateTime();
                mainItem.EmailAddress = item.EmailAddress;
                mainItem.ForceChangePassword = item.ForceChangePassword;
                mainItem.FullName = item.FullName;
                mainItem.IsInactive = item.IsInactive;
                mainItem.IsLocked = item.IsLocked;
                mainItem.Login = item.Login;
                mainItem.Password = item.Password;
                mainItem.PasswordUpdate = item.PasswordUpdate.ToDateTime();
                mainItem.PhoneNumber = item.PhoneNumber;
                mainItem.PrefferredLanguage = item.PrefferredLanguage;
                mainItem.TimeStamp = item.TimeStamp.ToByteArray();

                items.Add(mainItem);
            }

            _logic.Delete(items.ToArray());

            var result = new BoolReply { Result = true };

            var task = Task.FromResult(result);

            return task;
        }

        public override Task<SecurityLoginProto> GetSecurityLogin(IdRequest request, ServerCallContext context)
        {
            var item = _logic.Get(new Guid(request.Id));

            SecurityLoginProto result = new SecurityLoginProto();
            if (item != null)
            {
                result = new SecurityLoginProto
                {
                    Id = item.Id.ToString(),
                    AgreementAccepted = item.AgreementAccepted == null? null : Timestamp.FromDateTimeOffset(item.AgreementAccepted.Value.ToLocalTime()),
                    Created = Timestamp.FromDateTimeOffset(item.Created.ToLocalTime()),
                    EmailAddress = item.EmailAddress,
                    ForceChangePassword = item.ForceChangePassword,
                    FullName = item.FullName,
                    IsInactive = item.IsInactive,
                    IsLocked = item.IsLocked,
                    Login = item.Login,
                    Password = item.Password,
                    PasswordUpdate = item.PasswordUpdate == null ? null : Timestamp.FromDateTimeOffset(item.PasswordUpdate.Value.ToLocalTime()),
                    PhoneNumber = item.PhoneNumber,
                    PrefferredLanguage = item.PrefferredLanguage,
                    TimeStamp = Google.Protobuf.ByteString.CopyFrom(item.TimeStamp)
                };
            }

            var task = Task.FromResult(result);

            return task;
        }

        public override Task<SecurityLoginList> GetSecurityLogins(Empty request, ServerCallContext context)
        {
            var items = _logic.GetAll();

            var result = new SecurityLoginList();
            foreach (var item in items)
            {
                result.SecurityLogins.Add(new SecurityLoginProto
                {
                    Id = item.Id.ToString(),
                    AgreementAccepted = item.AgreementAccepted == null ? null : Timestamp.FromDateTimeOffset(item.AgreementAccepted.Value.ToLocalTime()),
                    Created = Timestamp.FromDateTimeOffset(item.Created.ToLocalTime()),
                    EmailAddress = item.EmailAddress,
                    ForceChangePassword = item.ForceChangePassword,
                    FullName = item.FullName,
                    IsInactive = item.IsInactive,
                    IsLocked = item.IsLocked,
                    Login = item.Login,
                    Password = item.Password,
                    PasswordUpdate = item.PasswordUpdate == null ? null : Timestamp.FromDateTimeOffset(item.PasswordUpdate.Value.ToLocalTime()),
                    PhoneNumber = item.PhoneNumber,
                    PrefferredLanguage = item.PrefferredLanguage,
                    TimeStamp = Google.Protobuf.ByteString.CopyFrom(item.TimeStamp)
                });
            }

            var task = Task.FromResult(result);

            return task;
        }

        public override Task<BoolReply> PostSecurityLogin(SecurityLoginList request, ServerCallContext context)
        {
            var items = new List<SecurityLoginPoco>();
            foreach (var item in request.SecurityLogins)
            {
                items.Add(new SecurityLoginPoco
                {
                    Id = new Guid(item.Id),
                    AgreementAccepted = item.AgreementAccepted.ToDateTime(),
                    Created = item.Created.ToDateTime(),
                    EmailAddress = item.EmailAddress,
                    ForceChangePassword = item.ForceChangePassword,
                    FullName = item.FullName,
                    IsInactive = item.IsInactive,
                    IsLocked = item.IsLocked,
                    Login = item.Login,
                    Password = item.Password,
                    PasswordUpdate = item.PasswordUpdate.ToDateTime(),
                    PhoneNumber = item.PhoneNumber,
                    PrefferredLanguage = item.PrefferredLanguage,
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

        public override Task<BoolReply> PutSecurityLogin(SecurityLoginList request, ServerCallContext context)
        {
            try
            {
                var items = new List<SecurityLoginPoco>();
                foreach (var item in request.SecurityLogins)
                {
                    var mainItem = _logic.Get(new Guid(item.Id));

                    mainItem.AgreementAccepted = item.AgreementAccepted.ToDateTime();
                    mainItem.Created = item.Created.ToDateTime();
                    mainItem.EmailAddress = item.EmailAddress;
                    mainItem.ForceChangePassword = item.ForceChangePassword;
                    mainItem.FullName = item.FullName;
                    mainItem.IsInactive = item.IsInactive;
                    mainItem.IsLocked = item.IsLocked;
                    mainItem.Login = item.Login;
                    mainItem.Password = item.Password;
                    mainItem.PasswordUpdate = item.PasswordUpdate.ToDateTime();
                    mainItem.PhoneNumber = item.PhoneNumber;
                    mainItem.PrefferredLanguage = item.PrefferredLanguage;
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
