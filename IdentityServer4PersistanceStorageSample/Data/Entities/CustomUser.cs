using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityServer4PersistanceStorageSample.Data.Entities
{
    public class CustomUser
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string SubjectId { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public string PasswordSalt { get; set; }

        [Required]
        public string Provider { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime LastUpdated { get; set; }
    }
}
