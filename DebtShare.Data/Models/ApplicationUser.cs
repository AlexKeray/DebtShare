using DebtShare.Data.Models.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DebtShare.Data.Models
{
    public class ApplicationUser : IdentityUser, IHasCreationDate
    {
        public string? Alias { get; set; }

        public DateTime? CreationDate { get; set; } = DateTime.UtcNow;

        public virtual ICollection<GroupUser>? GroupUsers { get; set; }
    }
}
