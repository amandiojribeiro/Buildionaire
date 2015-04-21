namespace Farfetch.Buildionaire.Domain.Model
{
    public class CodeCoverage
    {
        public int Id { get; set; }

        public int TotalBlocks { get; set; }

        public int CoveredBlocks { get; set; }

        public int TotalTests { get; set; }

        public int PassedTests { get; set; }

        public int Incomplete { get; set; }

        public virtual Build Build { get; set; }
    }
}
