namespace Farfetch.Buildionaire.Application.Services.DataImporter
{
    using System.Collections.Generic;
    using System.Linq;

    using Farfetch.Buildionaire.Application.Dto;
    using Farfetch.Buildionaire.Domain.Core.RepositoriesInterfaces;
    using Farfetch.Buildionaire.Domain.Model;
    using Farfetch.Buildionaire.Domain.Services;
    using Farfetch.Buildionaire.Infrastructure.CrossCutting.Adapters;

    public class ScoreDetailsService : IScoreDetailsService
    {

        private readonly IMetricRepository metricRepository;

        private readonly IScoreService scoreService;

        public ScoreDetailsService(IMetricRepository metricRepository, IScoreService scoreService)
        {
            this.metricRepository = metricRepository;
            this.scoreService = scoreService;
        }

        public List<BuildScoreDetailDto> CalulateScoreDetails(BuildDto build)
        {
            var metrics = this.metricRepository.GetAll().ToList();

            var buildScoreDetails = metrics.Select(x => this.scoreService.CalculateScoreDetail(TypeAdapterHelper.Adapt<Build>(build), TypeAdapterHelper.Adapt<Metric>(x))).ToList();
            return TypeAdapterHelper.Adapt<List<BuildScoreDetailDto>>(buildScoreDetails);
        }
    }
}
