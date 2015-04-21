namespace Farfetch.Buildionaire.Application.Services.DataImporter
{
    using Hangfire;

    public interface IImportCodeReviewServices
    {
        [Queue("importcodereview")]
        void ImportCodeReviews();
    }
}
