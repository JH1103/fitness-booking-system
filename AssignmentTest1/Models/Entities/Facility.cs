using System.ComponentModel.DataAnnotations;

namespace AssignmentTest1.Models.Entities
{
    public class Facility
    {
        [Key]
        public int FacilityId { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Facility Name")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Category { get; set; } = "Sports";

        [StringLength(500)]
        public string? Description { get; set; }

        [Range(1, 1000)]
        public int Capacity { get; set; } = 1;

        [Range(typeof(decimal), "0", "999999")]
        [Display(Name = "Hourly Price (RM)")]
        public decimal HourlyRate { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}