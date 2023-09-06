using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.Domain
{
    public class RuleName
    {
        public int RuleNameId { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Json is required.")]
        public string Json { get; set; }
        [Required(ErrorMessage = "Template is required.")]
        public string Template { get; set; }
        [Required(ErrorMessage = "SQL String is required.")]
        public string SqlStr { get; set; }
        [Required(ErrorMessage = "SQL Partition is required.")]
        public string SqlPart { get; set; }
    }
}