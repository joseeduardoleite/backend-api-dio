using System.Collections.Generic;

namespace WebAPI.Models
{
    public class FieldValidatorViewModelOutput
    {
        public IEnumerable<string> Errors { get; private set; }

        public FieldValidatorViewModelOutput(IEnumerable<string> errors)
        {
            Errors = errors;
        }
    }
}