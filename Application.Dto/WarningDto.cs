namespace Farfetch.Buildionaire.Application.Dto
{
    public class WarningDto
    {
        public int Id { get; set; }

        public WarningTypeDto Type { get; set; }

        public int Total { get; set; }

        public virtual BuildDto Build { get; set; }
    }
}
