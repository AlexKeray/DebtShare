using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DebtShare.Data.Models.Shared;

namespace DebtShare.Data.Models
{
    public class Expense : BaseModelExtended
    {
        [Required]
        public int GroupId { get; set; }

        [ForeignKey(nameof(GroupId))]
        public virtual required Group Group { get; set; }

        [Required]
        public required string PayerId { get; set; }

        [ForeignKey(nameof(PayerId))]
        public virtual required ApplicationUser Payer { get; set; }

        [Required]
        public required string CreatorId { get; set; }

        [ForeignKey(nameof(CreatorId))]
        public virtual required ApplicationUser Creator { get; set; }
    }
}
