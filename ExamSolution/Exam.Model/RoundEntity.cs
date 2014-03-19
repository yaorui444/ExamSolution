using System;

namespace Exam.Model
{
    public class RoundEntity
    {
        public int AppID { get; set; }

        public int RoundID { get; set; }

        public string ActorsCode { get; set; }

        public int Scores { get; set; }

        public DateTime Starttime { get; set; }

        public DateTime Endtime { get; set; }

        public int Usedtime { get; set; }
    }
}
