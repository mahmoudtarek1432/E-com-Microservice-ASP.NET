using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Exceptions
{
    public class ValidationException : ApplicationException
    {
        public ValidationException():
        base("One or more validation errors has occured") 
        {
            Errors = new Dictionary<string, string[]>();
        
        }

        public ValidationException(IEnumerable<ValidationFailure> failures)
        {
            Errors = failures
                .GroupBy(e=> e.PropertyName, e=> e.ErrorMessage)
                .ToDictionary(failiuregroup => failiuregroup.Key, failiuregroup=> failiuregroup.ToArray());
        }

        public IDictionary<string,string[]> Errors { get; }

    }
}
