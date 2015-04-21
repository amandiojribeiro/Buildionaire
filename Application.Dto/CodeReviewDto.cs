namespace Farfetch.Buildionaire.Application.Dto
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Win32;

    public class CodeReviewDto
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public virtual UserDto RequestedBy { get; set; }

        public int Type { get; set; }

        public int ExternalId { get; set; }
    }
}
