using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zoomrs.Models
{
    public class DisableDate
    {
        public int Id { get; set; }
        [Required]
        public DateOnly date { get; set; }

    }
}
