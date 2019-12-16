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
    }

    public enum ErrorStatusCodes
    {
        NOT_FOUND,
        INVALID_VALUE,
        ALREADY_EXISTS,
        INTERNAL_SERVER_ERROR
    }
}
