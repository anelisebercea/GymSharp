namespace GymSharp.Models
{
    public class Trainer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Specialization { get; set; }
        public string Qualification { get; set; }

        public ICollection<Exercise>? Exercises { get; set; }
    }
}
