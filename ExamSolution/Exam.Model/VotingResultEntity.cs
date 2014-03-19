namespace Exam.Model
{
    public class VotingResultEntity
    {
        public VoteEntity Vote { get; set; }

        public ActorEntity Actor { get; set; }

        public int Result { get; set; }
    }
}
