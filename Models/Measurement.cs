using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymSharp.Models
{
    public class Measurement
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int ExerciseID { get; set; }
        public DateTime Date { get; set; }
        public float Weight { get; set; }
        public float BodyFatPercentage { get; set; }
        public float Chest { get; set; }
        public float Waist { get; set; }
        public float Hips { get; set; }


        public User User { get; set; }
        public Exercise Exercise { get; set; }
    }
}
