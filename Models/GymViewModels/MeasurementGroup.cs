using System;
using System.ComponentModel.DataAnnotations;

namespace GymSharp.Models.GymViewModels
{
    public class MeasurementGroup
    {
        [DataType(DataType.Date)]
        public DateTime? Date { get; set; }
        public int ExerciseCount { get; set; }
    }
}
