﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment2.Models
{
    public class Expense
    {
        public long ExpenseId { get; set; }
        public long ModelId { get; set; }
        public long JobId { get; set; }
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }
        public string? Text { get; set; }
        [Column(TypeName = "decimal(9,2)")]
        public decimal amount { get; set; }
    }
}
