using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DebtShare.Data.Models.Shared;

namespace DebtShare.Data.Models
{
    public class Group : BaseModelExtended
    {
        [Required]
        public required string CreatorId { get; set; }

        [ForeignKey(nameof(CreatorId))]
        public virtual ApplicationUser Creator { get; set; } = null!;

        public virtual ICollection<GroupUser> GroupUsers { get; set; } = new List<GroupUser>();
    }
}
