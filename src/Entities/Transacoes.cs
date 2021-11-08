using System;
using System.ComponentModel.DataAnnotations;

namespace OFX.Entities
{
    public class Transacoes
    {
        [Required]
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime Data { get; set; }
        public string Description { get; set; }
        public string Operation { get; set; }
        public string Note { get; set; }
    }
}