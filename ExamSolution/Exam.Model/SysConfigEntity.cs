namespace Exam.Model
{
    public class SysConfigEntity
    {
        /// <summary>
        /// 实验标识
        /// </summary>
        public int AppID { get; set; }

        /// <summary>
        /// 轮次数
        /// </summary>
        public int RoundNumber { get; set; }

        /// <summary>
        /// 每轮次题目数
        /// </summary>
        public int RoundExams { get; set; }

        /// <summary>
        /// 轮次完成时间
        /// </summary>
        public int RoundLenght { get; set; }

        /// <summary>
        /// 答题时间
        /// </summary>
        public int Doingtime { get; set; }

        /// <summary>
        /// 信息驻留时间
        /// </summary>
        public int Readingtime { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public bool State { get; set; }
    }
}
