namespace Farfetch.Buildionaire.Application.Dto
{
    using System.Collections.Generic;

    public class ProjectDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<BuildDto> Builds { get; set; }
    }
}
