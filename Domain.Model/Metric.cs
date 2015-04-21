namespace Farfetch.Buildionaire.Domain.Model
{
    using System.Collections.Generic;

    public class Metric
    {
        public const string CodeCoverage = "CODECOVERAGE";
        public const string Error = "ERROR";
        public const string CodeReview = "CODEREVIEW";
        public const string Warning = "WARNING";
        public const string BuildStatus = "BUILDSTATUS";

        public int Id { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public int Weight { get; set; }

        public virtual ICollection<BuildScoreDetail> BuildScoreDetails { get; set; }
    }
}
