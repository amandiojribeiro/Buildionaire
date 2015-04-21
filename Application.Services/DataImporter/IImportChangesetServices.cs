namespace Farfetch.Buildionaire.Application.Services.DataImporter
{
    using Hangfire;

    public interface IImportChangesetServices
    {
        [Queue("importchangesets")]
        void ImportChangesets();
    }
}