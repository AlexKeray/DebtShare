using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DebtShare.Data.Models.Shared;

namespace DebtShare.Data.Models
{
    public class Group : BaseModelExtended
    {
        [Required]
        public required string CreatorId { get; set; }

        [ForeignKey(nameof(CreatorId))]
        public virtual required ApplicationUser Creator { get; set; }

        public virtual ICollection<GroupUser> GroupUsers { get; set; } = new List<GroupUser>();
    }
}
