using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FanSelector.Models.Db
{
    public partial class Client
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Client()
        {
            RefreshTokens = new HashSet<RefreshToken>();
        }

        [StringLength(50)]
        public string Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Secret { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public int ApplicationTypeId { get; set; }

        public bool? IsActive { get; set; }

        public int? RefreshTokenLifeTime { get; set; }

        [StringLength(250)]
        public string AllowedOrigin { get; set; }

        public virtual ApplicationType ApplicationType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
    }
}
