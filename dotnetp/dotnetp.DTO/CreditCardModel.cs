
using System.ComponentModel.DataAnnotations;

namespace dotnetp
{
    public class CreditCardModel
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        public string CardNumber { get; set; }
        
        [Required]
        public string CVV { get; set; }
        
        [Required]
        public string EPayment { get; set; }
    }
}
