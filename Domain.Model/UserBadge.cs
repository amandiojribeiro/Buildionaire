namespace Farfetch.Buildionaire.Domain.Model
{
    using System;

    public class UserBadge
    {
        public DateTime ReceivedDate { get; set; }

        public virtual Badge Badge { get; set; }

        public int Id { get; set; }
    }
}