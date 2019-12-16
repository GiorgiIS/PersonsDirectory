using System;
using System.Collections.Generic;
using System.Text;

namespace TBCPersonsDirectory.Common
{
    public class ServiceResponse
    {
        public bool IsSuccess { get; protected set; }
        public ServiceErrorMessage ServiceErrorMessage { get; protected set; }

        public ServiceResponse Ok()
        {
            return new ServiceResponse
            {
                IsSuccess = true
            };
        }

        public ServiceResponse Fail(ServiceErrorMessage ServiceErrorMessage)
        {
            return new ServiceResponse
            {
                IsSuccess = false,
                ServiceErrorMessage = ServiceErrorMessage
            };
        }
    }

    public class ServiceResponse<T> : ServiceResponse
    {
        public T Result { get; private set; }

        public ServiceResponse<T> Ok(T successObj)
        {
            return new ServiceResponse<T>
            {
                IsSuccess = true,
                Result = successObj,
            };
        }

        public new ServiceResponse<T> Fail(ServiceErrorMessage errorMessage)
        {
            return new ServiceResponse<T>
            {
                IsSuccess = false,
                ServiceErrorMessage = errorMessage
            };
        }
    }

    public class ServiceErrorMessage
    {
        public ErrorStatusCodes Code { get; set; }
        public string Description { get; set; }

        public ServiceErrorMessage NotFound(string subject) => new ServiceErrorMessage { Code = ErrorStatusCodes.NOT_FOUND, Description = $"{subject} Was not found" };

        public ServiceErrorMessage InvalidValue(string subject) => new ServiceErrorMessage { Code = ErrorStatusCodes.INVALID_VALUE, Description = $"{subject} Was Invalid" };

        public ServiceErrorMessage AlreadyExists(string subject) => new ServiceErrorMessage { Code = ErrorStatusCodes.ALREADY_EXISTS, Description = $"{subject} Already exists" };

        public ServiceErrorMessage ChangesNotSaved(string source) => new ServiceErrorMessage { Code = ErrorStatusCodes.CHANGES_NOT_SAVED, Description = $"Changes were not saved in {source}" };

    }

    public enum ErrorStatusCodes
    {
        DEFAULT,
        NOT_FOUND,
        INVALID_VALUE,
        ALREADY_EXISTS,
        INTERNAL_SERVER_ERROR,
        CHANGES_NOT_SAVED
    }
}
