namespace Farfetch.Buildionaire.Application.Dto
{
    public class CodeCoverageDto
    {
        public int Id { get; set; }

        public int TotalBlocks { get; set; }

        public int CoveredBlocks { get; set; }

        public int TotalTests { get; set; }

        public int PassedTests { get; set; }

        public virtual BuildDto Build { get; set; }
    }
}
