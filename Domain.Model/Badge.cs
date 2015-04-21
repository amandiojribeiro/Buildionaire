namespace Farfetch.Buildionaire.Domain.Model
{
    public class Badge
    {
        public int Id { get; set; }

        public virtual BadgeType BadgeType { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public int Min { get; set; }

        public int Max { get; set; }
    }
}
