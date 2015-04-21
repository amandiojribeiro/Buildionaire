namespace Farfetch.Buildionaire.Domain.Model
{
    using System.Collections.Generic;

    public class Project
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<ChangeSet> ChangeSets { get; set; }

        public virtual ICollection<Build> Builds { get; set; }

        public virtual ICollection<Dashboard> Dashboards { get; set; }
    }
}
