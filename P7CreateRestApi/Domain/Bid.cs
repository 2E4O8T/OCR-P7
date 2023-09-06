using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.Domain
{
    public class Bid
    {
        public int BidId { get; set; }
        [Required(ErrorMessage = "Account is required.")]
        public string Account { get; set; }
        [Required(ErrorMessage = "Bid Type is required.")]
        public string BidType { get; set; }
        public double? BidQuantity { get; set; }
        public double? AskQuantity { get; set; }
        public double? BidAmount { get; set; }
        public double? AskAmount { get; set; }
        [Required(ErrorMessage = "Benchmark is required.")]
        public string Benchmark { get; set; }
        public DateTime? BidListDate { get; set; }
        [Required(ErrorMessage = "Commentary is required.")]
        public string Commentary { get; set; }
        [Required(ErrorMessage = "Bid Security is required.")]
        public string BidSecurity { get; set; }
        [Required(ErrorMessage = "Bid Status is required.")]
        public string BidStatus { get; set; }
        [Required(ErrorMessage = "Trader is required.")]
        public string Trader { get; set; }
        [Required(ErrorMessage = "Book is required.")]
        public string Book { get; set; }
        [Required(ErrorMessage = "Creation Name is required.")]
        public string CreationName { get; set; }
        public DateTime? CreationDate { get; set; }
        [Required(ErrorMessage = "Revision Name is required.")]
        public string RevisionName { get; set; }
        public DateTime? RevisionDate { get; set; }
        [Required(ErrorMessage = "Deal Name is required.")]
        public string DealName { get; set; }
        [Required(ErrorMessage = "Deal Type is required.")]
        public string DealType { get; set; }
        [Required(ErrorMessage = "Source List Id is required.")]
        public string SourceListId { get; set; }
        [Required(ErrorMessage = "Side is required.")]
        public string Side { get; set; }
    }
    
}