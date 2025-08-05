using System;
using System.ComponentModel.DataAnnotations;

namespace BillBuddy
{
    public class Bill
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public DateTime DueDate { get; set; }
        [Required]
        [StringLength(50)]
        public string Category { get; set; }
    }
}
