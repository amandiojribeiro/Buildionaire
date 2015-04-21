namespace Farfetch.Buildionaire.Application.Dto
{
    using System.Collections.Generic;

    public class UserWithBadgesDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<BadgeDto> Badges { get; set; }
    }
}
