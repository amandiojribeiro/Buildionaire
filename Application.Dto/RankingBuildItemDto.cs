namespace Farfetch.Buildionaire.Application.Dto
{
    using System;

    public class RankingBuildItemDto
    {
        public int Id { get; set; }

        public int Position { get; set; }

        public string Name { get; set; }

        public DateTime BuildDate { get; set; }

        public int Score { get; set; }
    }
}
