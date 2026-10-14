namespace leaners 
{
    public class Learner
    {
        public int Id;
        public string Name;
        public string Contact;
        public string CourseCode;
        public decimal FinalFee;
        public bool IsPaid;

        public Learner(int id, string name, string contact)
        {
            Id = id;
            Name = name;
            Contact = contact;
            CourseCode = "";
            FinalFee = 0;
            IsPaid = false;
        }
    }
}