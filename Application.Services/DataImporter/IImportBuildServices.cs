namespace Farfetch.Buildionaire.Application.Services.DataImporter
{
    using System;

    using Hangfire;

    public interface IImportBuildServices
    {
        [Queue("importbuilds")]
        void ImportBuilds();

        void ImportBuilds(DateTime since);
    }
}
