namespace Farfetch.Buildionaire.Domain.Model
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Win32;

    public class CodeReview
    {
        public int Id { get; set; }

        public int ExternalId { get; set; }

        public DateTime CreatedAt { get; set; }

        public CodeReviewType Type { get; set; }

        public virtual User RequestedBy { get; set; }
    }
}
