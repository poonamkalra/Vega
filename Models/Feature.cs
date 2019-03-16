using System.ComponentModel.DataAnnotations;
namespace vega.Models
{
    public class Feature
    {
        [StringLength(255)]
        [Required]
        public string Name{get;set;}
        public int  Id{get;set;}
    }
}