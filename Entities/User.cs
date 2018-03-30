using System.ComponentModel.DataAnnotations;

namespace ContactManager.Entities
{
    public class User
    {
        public long? Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}