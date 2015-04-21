namespace Farfetch.Buildionaire.Application.Dto
{
    using System.Collections.Generic;

    public class MetricDto
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public int Weight { get; set; }

        public virtual ICollection<BuildScoreDetailDto> BuildScoreDetails { get; set; }
    }
}
