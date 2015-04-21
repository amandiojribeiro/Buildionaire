namespace Farfetch.Buildionaire.Application.Dto
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    public class CodeReviewInteractionDto
    {
        public int Id { get; set; }

        public UserDto User { get; set; }

        public DateTime CreatedAt { get; set; }

        public CodeReviewInteractionTypeDto InteractionType { get; set; }

        public virtual CodeReviewDto CodeReview { get; set; }
    }
}
