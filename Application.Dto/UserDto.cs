namespace Farfetch.Buildionaire.Application.Dto
{
    using System.Collections.Generic;

    public class UserDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<CodeReviewDto> CodeReviews { get; set; }
    }
}
