namespace Farfetch.Buildionaire.Domain.Model
{
    using System.Collections.Generic;

    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<CodeReview> CodeReviews { get; set; }

        public virtual ICollection<UserBadge> Badges { get; set; }

        public virtual ICollection<ChangeSet> ChangeSets { get; set; }
    }
}
