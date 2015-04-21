namespace Farfetch.Buildionaire.Domain.Model
{
    public class BuildScoreDetail
    {
        public int Id { get; set; }

        public virtual Build Build { get; set; }

        public virtual Metric Metric { get; set; }

        public int Score { get; set; }
    }
}
