namespace Farfetch.Buildionaire.Domain.Model
{
    using System;
    using System.Collections.Generic;

    public class Build
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string BuildNumber { get; set; }

        public BuildStatus Status { get; set; }

        public int TotalScore { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime CompletedAt { get; set; }

        public virtual Project Project { get; set; }

        public virtual ICollection<Warning> Warnings { get; set; }

        public virtual Error Error { get; set; }

        public virtual CodeCoverage CodeCoverage { get; set; }

        public ICollection<BuildScoreDetail> BuildScoreDetails { get; set; }

        public virtual ICollection<CodeReview> CodeReviews { get; set; }
    }
}
