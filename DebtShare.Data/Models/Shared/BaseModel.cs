using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebtShare.Data.Models.Shared
{
    public class BaseModel
    {
        [Required]
        public int Id { get; set; }
    }
}
