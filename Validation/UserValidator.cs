using System;
using System.Collections.Generic;
using System.Linq;
using ContactManager.Entities;

namespace ContactManager.Validation
{
    public class UserValidator : IValidator<User>
    {
        private readonly List<string> _errors = new List<string>();
        private readonly Dictionary<string, Func<User, string>> _funcs = new Dictionary<string, Func<User, string>>
            {
                {"Email", u => u.Email},
                {"First name", u => u.FirstName},
                {"Last name", u => u.LastName}
            };
        
        public string[] Errors => _errors.ToArray();

        public bool Validate(User entity)
        {
            _errors.Clear();

            return _funcs.Aggregate(true, (acc, kvp) =>
            {
                var (invalid, error) = IsNullOrWhitespace(kvp.Key, kvp.Value(entity));
                if (error != null) _errors.Add(error);

                return acc && !invalid;
            });

            // Local Funcs
            (bool, string) IsNullOrWhitespace(string field, string value) => string.IsNullOrWhiteSpace(value)
                ? (true, $"{field} is a required field")
                : (false, null);
        }
    }
}