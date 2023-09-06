using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.Domain
{
    public class Rating
    {
        public int RatingId { get; set; }
        [Required(ErrorMessage = "Moody's Rating is required.")]
        public string MoodysRating { get; set; }
        [Required(ErrorMessage = "Standard and Poor's is required.")]
        public string SandPRating { get; set; }
        [Required(ErrorMessage = "Fitch Rating is required.")]
        public string FitchRating { get; set; }
        public byte? OrderNumber { get; set; }
    }
}