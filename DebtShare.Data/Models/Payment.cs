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
        public virtual required ApplicationUser Payer { get; set; }

        [Required]
        public required string ReceiverId { get; set; }

        [ForeignKey(nameof(ReceiverId))]
        public virtual required ApplicationUser Receiver { get; set; }

        public DateTime? CreationDate { get; set; } = DateTime.UtcNow;

        public string? Description { get; set; }

        [Required]
        public bool IsConfirmed { get; set; } = false;
    }
}
