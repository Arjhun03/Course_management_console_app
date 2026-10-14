namespace courceses
{
   public  class Course
    {
        public string Code;
        public string Title;
        public decimal Fee;
        public int Duration;
        public int Seats;

        public Course(string code, string title, decimal fee,
            int duration, int seats)
        {
            Code = code;
            Title = title;
            Fee = fee;
            Duration = duration;
            Seats = seats;
        }
    }
}