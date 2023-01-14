using System.Security.Policy;

namespace GymSharp.Models
{
    public class WorkoutPlan
    {
        public int MuscleGroupID { get; set; }
        public int ExerciseID { get; set; }
        public MuscleGroup MuscleGroup { get; set; }
        public Exercise Exercise { get; set; }

    }
}
