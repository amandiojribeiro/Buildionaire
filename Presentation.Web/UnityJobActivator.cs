namespace Farfetch.Buildionaire.Presentation.Web
{
    using System;

    using Hangfire;

    using Microsoft.Practices.Unity;

    public class UnityJobActivator : JobActivator
    {
        private readonly IUnityContainer container;

        public UnityJobActivator(IUnityContainer container)
        {
            this.container = container;
        }

        public override object ActivateJob(Type jobType)
        {
            return this.container.Resolve(jobType);
        }
    }
}