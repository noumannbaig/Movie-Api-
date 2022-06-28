using System;
using System.Globalization;
using FluentValidation.Results;
using System.Collections.Generic;
using System.Text;

namespace Backend.Application.Exceptions
{
    public class TokenExpiryException : Exception
    {
        public TokenExpiryException() : base("You token has been expired.")
        {
            Errors = new List<ErrorModel>();
        }

        public List<ErrorModel> Errors { get; }

        public TokenExpiryException(IEnumerable<ValidationFailure> failures)
            : this()
        {
            foreach (var failure in failures)
            {
                Errors.Add(new ErrorModel()
                {
                    propertyName = failure.PropertyName,
                    errorMessage = failure.ErrorMessage,
                });
            }
        }
    }

}
