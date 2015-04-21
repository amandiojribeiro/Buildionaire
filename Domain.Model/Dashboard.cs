namespace Farfetch.Buildionaire.Domain.Model
{
    using System.Collections.Generic;

    public class Dashboard
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
    }
}
