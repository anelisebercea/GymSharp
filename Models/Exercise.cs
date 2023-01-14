using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Threading.Tasks;

namespace GymSharp.Models
{
    public class Exercise
    {
        public int ID { get; set; }
        public string ExerciseName { get; set; }
        public int TrainerId { get; set; }
        public Trainer? Trainer { get; set; }
        public string Description { get; set; }
        public string MuscleGroup { get; set; }
        public string DifficultyLevel { get; set; }



        public ICollection<Measurement>? Measurements { get; set; }
        public ICollection<WorkoutPlan> WorkoutPlans { get; set; }

    }
}
