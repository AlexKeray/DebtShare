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
    public class Payment : BaseModel, IHasCreationDate
    {
        [Required]
        public required string PayerId { get; set; }

        [ForeignKey(nameof(PayerId))]
        public virtual ApplicationUser Payer { get; set; } = null!;

        [Required]
        public required string ReceiverId { get; set; }

        [ForeignKey(nameof(ReceiverId))]
        public virtual ApplicationUser Receiver { get; set; } = null!;

        public DateTime? CreationDate { get; set; } = DateTime.UtcNow;

        public string? Description { get; set; }

        [Required]
        public bool IsConfirmed { get; set; } = false;
    }
}
