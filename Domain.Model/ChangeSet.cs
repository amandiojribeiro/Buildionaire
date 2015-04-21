namespace Farfetch.Buildionaire.Domain.Model
{
    using System;

    public class ChangeSet
    {
        public int Id { get; set; }

        public User User { get; set; }

        public DateTime CreatedAt { get; set; }

        public int ExternalChangesetId { get; set; }

        public virtual Project Project { get; set; }
    }
}