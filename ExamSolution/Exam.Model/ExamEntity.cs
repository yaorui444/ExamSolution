using System;

namespace Exam.Model
{
    public class ExamEntity
    {
        public int AppID { get; set; }

        public BraindumpEntity Question { get; set; }

        public RoundEntity Round { get; set; }

        public int Serial { get; set; }

        public ActorEntity Actor { get; set; }

        public int Scores { get; set; }

        public bool Correct { get; set; }

        public DateTime Starttime { get; set; }

        public DateTime Endtime { get; set; }

        public int Usedtime { get; set; }

        public string DoingVideo { get; set; }

        public string Dvpath { get; set; }

        public string ReadingVideo { get; set; }

        public string Rvpath { get; set; }
    }
}
