namespace Farfetch.Buildionaire.Domain.Model
{
    using System.Collections.Generic;

    public class BadgeType
    {
        public const string Checkin = "CHECKIN";

        public const string CodeReviewRequest = "CODEREVIEWREQUEST";

        public const string CodeReviewResponse = "CODEREVIEWRESPONSE";

        public int Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Badge> Badges { get; set; }
    }
}
