namespace Exam.Model
{
    public class BraindumpEntity
    {
        /// <summary>
        /// 标识
        /// </summary>
        public int ExamID { get; set; }

        /// <summary>
        /// 考题
        /// </summary>
        public string Question { get; set; }

        /// <summary>
        /// 答案
        /// </summary>
        public double Answer { get; set; }

        /// <summary>
        /// 分数
        /// </summary>
        public int Scores { get; set; }

        /// <summary>
        /// 难度系数
        /// </summary>
        public int Diff { get; set; }
    }
}
