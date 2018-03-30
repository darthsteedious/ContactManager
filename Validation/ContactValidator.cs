using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ContactManager.Validation
{
    public class ContactValidator : IValidator<Contact>
    {
        private const string FirstName = "First name";
        private const string LastName = "Last name";
        
        private readonly List<(string, Func<Contact, string>)> _funcs = new List<(string, Func<Contact, string>)>
        {
            (FirstName, c => c.FirstName),
            (LastName, c => c.LastName)
        };

        private readonly List<string> _errors = new List<string>();

        public string[] Errors => _errors.ToArray();

        public bool Validate(Contact entity)
        {
            _errors.Clear();
       
            return _funcs.Aggregate(true, (acc, t) =>
            {
                var (invalid, error) = IsNullOrWhitespace(t.Item1, t.Item2(entity));
                if (error != null) _errors.Add(error);

                return acc && !invalid;
            });
                
            (bool, string) IsNullOrWhitespace(string field, string value) =>
                string.IsNullOrWhiteSpace(value) ? (true, $"{field} is a required field.") : (false, null);
        }
    }
}