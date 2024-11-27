using FluentValidation.Results;

namespace FamilyManager.Application.Common.Exceptions
{
    public class ValidationExeption : Exception
    {
        public IDictionary<string, string[]> Errors { get; }
        public ValidationExeption() : base("One or more validstion errors have occured. See the list below.")
        {
            Errors = new Dictionary<string, string[]>();
        }
        public ValidationExeption(IEnumerable<ValidationFailure> failures) : this()
        {
            Errors = failures
                .GroupBy(f => f.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());

        }
    }
}
