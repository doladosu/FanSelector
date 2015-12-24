using System;
using System.ComponentModel.DataAnnotations;

namespace FanSelector.Models.Db
{
    public partial class RefreshToken
    {
        [StringLength(50)]
        public string Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Subject { get; set; }

        [Required]
        [StringLength(50)]
        public string ClientId { get; set; }

        public DateTime? IssuedUtc { get; set; }

        public DateTime? ExpiresUtc { get; set; }

        public bool? IsActive { get; set; }

        [Required]
        [StringLength(250)]
        public string ProtectedTicket { get; set; }

        public virtual Client Client { get; set; }
    }
}
