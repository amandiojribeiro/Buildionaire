namespace Farfetch.Buildionaire.Domain.Model
{
    public class Warning
    {
        public int Id { get; set; }

        public WarningType Type { get; set; }

        public int Total { get; set; }

        public virtual Build Build { get; set; }
    }
}
