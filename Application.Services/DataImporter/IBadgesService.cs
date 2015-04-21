namespace Farfetch.Buildionaire.Application.Services.DataImporter
{
    using Hangfire;

    public interface IBadgesService
    {
        [Queue("calculatebadges")]
        void CalculateBadges();
    }
}