using System.ComponentModel.DataAnnotations.Schema;
using DebtShare.Data.Models.Shared;

namespace DebtShare.Data.Models
{
    public class RefreshToken : BaseModel, IHasCreationDate
    {

        public required string Value { get; set; }

        public required string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; } = null!;

        public DateTime? CreationDate { get; set; } = DateTime.UtcNow;

        public DateTime ExpirationDate { get; set; } = DateTime.UtcNow.AddDays(7);

        public DateTime? RevocationDate { get; set; }

        public bool IsRevoked { get; set; } = false;

        
    }
}
