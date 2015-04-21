namespace Farfetch.Buildionaire.Application.Dto
{
    public class BuildScoreDetailDto
    {
        public int Id { get; set; }

        public virtual BuildDto Build { get; set; }

        public virtual MetricDto Metric { get; set; }

        public int Score { get; set; }
    }
}
