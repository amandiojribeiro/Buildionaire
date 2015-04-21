namespace Farfetch.Buildionaire.Domain.Model
{
    public class Error
    {
        public int Id { get; set; }

        public int Total { get; set; }

        public virtual Build Build { get; set; }
    }
}
