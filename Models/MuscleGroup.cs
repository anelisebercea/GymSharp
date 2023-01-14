using System.ComponentModel.DataAnnotations;

namespace GymSharp.Models
{
    public class MuscleGroup
    {
        public int ID { get; set; }
        [Required]
        [Display(Name = "Muscle Group Name")]
        [StringLength(50)]
        public string Group { get; set; }
        
        public ICollection<WorkoutPlan> WorkoutPlans { get; set; }

    }
}
