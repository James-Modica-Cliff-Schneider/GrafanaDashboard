using System;

namespace Model
{
    public class MostCommonProblem
    {
        public string ProblemName { get; set; }
        public DateTime Date { get; set; }
        public int ProblemCount { get; set; }

        public MostCommonProblem(string problemName, int problemCount)
        {
            ProblemName = problemName;
            Date = DateTime.Now;
            ProblemCount = problemCount;


        }
    }
}