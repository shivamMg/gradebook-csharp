namespace GradeBook
{
    public class Statistics
    {
        public double Average
        {
            get
            {
                return Sum / Count;
            }
        }
        public double Sum;
        public int Count;
        public double Highest;
        public double Lowest;

        public Statistics()
        {
            Highest = double.MinValue;
            Lowest = double.MaxValue;
            Sum = 0.0;
            Count = 0;
        }

        public void Add(double number)
        {
            Sum += number;
            Count += 1;
            Lowest = Math.Min(Lowest, number);
            Highest = Math.Max(Highest, number);
        }
    }
}