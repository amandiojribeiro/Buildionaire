namespace Farfetch.Buildionaire.Application.Dto
{
    public class ErrorDto
    {
        public int Id { get; set; }

        public int Total { get; set; }

        public virtual BuildDto Build { get; set; }
    }
}
