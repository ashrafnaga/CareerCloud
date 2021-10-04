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
    public class ApplicantProfileService : ApplicantProfileProvider.ApplicantProfileProviderBase
    {
        ApplicantProfileLogic _logic;

        public ApplicantProfileService()
        {
            var repository = new EFGenericRepository<ApplicantProfilePoco>();
            _logic = new ApplicantProfileLogic(repository);
        }

        public override Task<BoolReply> DeleteApplicantProfile(ApplicantProfileList request, ServerCallContext context)
        {
            var items = new List<ApplicantProfilePoco>();
            foreach (var item in request.ApplicantProfiles)
            {
                var mainItem = _logic.Get(new Guid(item.Id));

                mainItem.City = item.City;
                mainItem.Country = item.Country;
                mainItem.Currency = item.Currency;
                mainItem.CurrentRate = (decimal?)item.CurrentRate;
                mainItem.CurrentSalary = (decimal?)item.CurrentSalary;
                mainItem.Login = new Guid(item.Login);
                mainItem.PostalCode = item.PostalCode;
                mainItem.Province = item.Province;
                mainItem.Street = item.Street;
                mainItem.TimeStamp = item.TimeStamp.ToByteArray();

                items.Add(mainItem);
            }

            _logic.Delete(items.ToArray());

            var result = new BoolReply { Result = true };

            var task = Task.FromResult(result);

            return task;
        }

        public override Task<ApplicantProfileProto> GetApplicantProfile(IdRequest request, ServerCallContext context)
        {
            var item = _logic.Get(new Guid(request.Id));

            ApplicantProfileProto result = new ApplicantProfileProto();
            if (item != null)
            {
                result = new ApplicantProfileProto
                {
                    Id = item.Id.ToString(),
                    City = item.City,
                    Country = item.Country,
                    Currency = item.Currency,
                    CurrentRate = (double?)item.CurrentRate,
                    CurrentSalary = (double?)item.CurrentSalary,
                    Login = item.Login.ToString(),
                    PostalCode = item.PostalCode,
                    Province = item.Province,
                    Street = item.Street,
                    TimeStamp = Google.Protobuf.ByteString.CopyFrom(item.TimeStamp)
                };
            }

            var task = Task.FromResult(result);

            return task;
        }

        public override Task<ApplicantProfileList> GetApplicantProfiles(Empty request, ServerCallContext context)
        {
            var items = _logic.GetAll();

            var result = new ApplicantProfileList();
            foreach (var item in items)
            {
                result.ApplicantProfiles.Add(new ApplicantProfileProto
                {
                    Id = item.Id.ToString(),
                    City = item.City,
                    Country = item.Country,
                    Currency = item.Currency,
                    CurrentRate = (double?)item.CurrentRate,
                    CurrentSalary = (double?)item.CurrentSalary,
                    Login = item.Login.ToString(),
                    PostalCode = item.PostalCode,
                    Province = item.Province,
                    Street = item.Street,
                    TimeStamp = Google.Protobuf.ByteString.CopyFrom(item.TimeStamp)
                });
            }

            var task = Task.FromResult(result);

            return task;
        }

        public override Task<BoolReply> PostApplicantProfile(ApplicantProfileList request, ServerCallContext context)
        {
            var items = new List<ApplicantProfilePoco>();
            foreach (var item in request.ApplicantProfiles)
            {
                items.Add(new ApplicantProfilePoco
                {
                    Id = new Guid(item.Id),
                    City = item.City,
                    Country = item.Country,
                    Currency = item.Currency,
                    CurrentRate = (decimal?)item.CurrentRate,
                    CurrentSalary = (decimal?)item.CurrentSalary,
                    Login = new Guid(item.Login),
                    PostalCode = item.PostalCode,
                    Province = item.Province,
                    Street = item.Street,
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

        public override Task<BoolReply> PutApplicantProfile(ApplicantProfileList request, ServerCallContext context)
        {
            try
            {
                var items = new List<ApplicantProfilePoco>();
                foreach (var item in request.ApplicantProfiles)
                {
                    var mainItem = _logic.Get(new Guid(item.Id));

                    mainItem.City = item.City;
                    mainItem.Country = item.Country;
                    mainItem.Currency = item.Currency;
                    mainItem.CurrentRate = (decimal?)item.CurrentRate;
                    mainItem.CurrentSalary = (decimal?)item.CurrentSalary;
                    mainItem.Login = new Guid(item.Login);
                    mainItem.PostalCode = item.PostalCode;
                    mainItem.Province = item.Province;
                    mainItem.Street = item.Street;
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
