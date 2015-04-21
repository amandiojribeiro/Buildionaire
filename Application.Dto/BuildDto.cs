namespace Farfetch.Buildionaire.Application.Dto
{
    using System;
    using System.Collections.Generic;
    
    public class BuildDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string BuildNumber { get; set; }

        public BuildStatusDto Status { get; set; }

        public int TotalScore { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime CompletedAt { get; set; }

        public virtual ProjectDto Project { get; set; }

        public virtual ICollection<WarningDto> Warnings { get; set; }

        public virtual ErrorDto Error { get; set; }

        public virtual CodeCoverageDto CodeCoverage { get; set; }

        public ICollection<BuildScoreDetailDto> BuildScoreDetails { get; set; }

        public virtual ICollection<CodeReviewDto> CodeReviews { get; set; }
    }
}
