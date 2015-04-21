namespace Farfetch.Buildionaire.Application.Services.TypeAdapters
{
    using AutoMapper;

    using Farfetch.Buildionaire.Application.Dto;
    using Farfetch.Buildionaire.Domain.Model;

    public class DtoAdapterProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Build, BuildDto>()
            .ForMember(dest => dest.BuildNumber, opt => opt.MapFrom(e => e.BuildNumber))
            .ForMember(dest => dest.BuildScoreDetails, opt => opt.MapFrom(e => e.BuildScoreDetails))
            .ForMember(dest => dest.CodeCoverage, opt => opt.MapFrom(e => e.CodeCoverage))
            .ForMember(dest => dest.CodeReviews, opt => opt.MapFrom(e => e.CodeReviews))
            .ForMember(dest => dest.CompletedAt, opt => opt.MapFrom(e => e.CompletedAt))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(e => e.CreatedAt))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(e => e.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(e => e.Name))
            .ForMember(dest => dest.Project, opt => opt.MapFrom(e => e.Project))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(e => e.Status))
            .ForMember(dest => dest.TotalScore, opt => opt.MapFrom(e => e.TotalScore))
            .ForMember(dest => dest.Warnings, opt => opt.MapFrom(e => e.Warnings))            
            .ReverseMap();

            Mapper.CreateMap<BuildScoreDetail, BuildScoreDetailDto>()
            .ForMember(dest => dest.Build, opt => opt.MapFrom(e => e.Build))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(e => e.Id))
            .ForMember(dest => dest.Metric, opt => opt.MapFrom(e => e.Metric))
            .ForMember(dest => dest.Score, opt => opt.MapFrom(e => e.Score))
            .ReverseMap();

            Mapper.CreateMap<CodeCoverage, CodeCoverageDto>()
            .ForMember(dest => dest.Build, opt => opt.MapFrom(e => e.Build))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(e => e.Id))
            .ForMember(dest => dest.CoveredBlocks, opt => opt.MapFrom(e => e.CoveredBlocks))
            .ForMember(dest => dest.PassedTests, opt => opt.MapFrom(e => e.PassedTests))
            .ForMember(dest => dest.TotalBlocks, opt => opt.MapFrom(e => e.TotalBlocks))
            .ForMember(dest => dest.TotalTests, opt => opt.MapFrom(e => e.TotalTests))
            .ReverseMap();

            Mapper.CreateMap<CodeReview, CodeReviewDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(e => e.Id))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(e => e.CreatedAt))
            .ForMember(dest => dest.RequestedBy, opt => opt.MapFrom(e => e.RequestedBy))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(e => e.Type))
            .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(e => e.ExternalId))
            .ReverseMap();

            Mapper.CreateMap<UserBadge, BadgeDto>()
            .ForMember(dest => dest.Code, opt => opt.MapFrom(e => e.Badge.Code))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(e => e.Badge.Name))
            .ForMember(dest => dest.ReceivedAt, opt => opt.MapFrom(e => e.ReceivedDate))
            .ReverseMap();

            Mapper.CreateMap<Error, ErrorDto>()
            .ForMember(dest => dest.Build, opt => opt.MapFrom(e => e.Build))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(e => e.Id))
            .ForMember(dest => dest.Total, opt => opt.MapFrom(e => e.Total))
            .ReverseMap();

            Mapper.CreateMap<Metric, MetricDto>()
            .ForMember(dest => dest.BuildScoreDetails, opt => opt.MapFrom(e => e.BuildScoreDetails))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(e => e.Id))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(e => e.Description))
            .ForMember(dest => dest.Weight, opt => opt.MapFrom(e => e.Weight))
            .ReverseMap();

            Mapper.CreateMap<User, UserDto>()
            .ForMember(dest => dest.CodeReviews, opt => opt.MapFrom(e => e.CodeReviews))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(e => e.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(e => e.Name))
            .ReverseMap();

            Mapper.CreateMap<User, UserWithBadgesDto>()
            .ForMember(dest => dest.Badges, opt => opt.MapFrom(e => e.Badges))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(e => e.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(e => e.Name))
            .ReverseMap();

            Mapper.CreateMap<Warning, WarningDto>()
            .ForMember(dest => dest.Build, opt => opt.MapFrom(e => e.Build))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(e => e.Id))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(e => e.Type))
            .ForMember(dest => dest.Total, opt => opt.MapFrom(e => e.Total))
            .ReverseMap();

            Mapper.CreateMap<Project, ProjectDto>()
           .ForMember(dest => dest.Builds, opt => opt.MapFrom(e => e.Builds))
           .ForMember(dest => dest.Id, opt => opt.MapFrom(e => e.Id))
           .ForMember(dest => dest.Name, opt => opt.MapFrom(e => e.Name))
           .ReverseMap();

            Mapper.CreateMap<Build, RankingBuildItemDto>()
            .ForMember(dest => dest.BuildDate, opt => opt.MapFrom(e => e.CreatedAt))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(e => e.Id))
            .ForMember(dest => dest.Score, opt => opt.MapFrom(e => e.TotalScore))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(e => e.Name))
            .ReverseMap();

            Mapper.CreateMap<User, RankingReviewerItemDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(e => e.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(e => e.Name))
            .ReverseMap();

            Mapper.CreateMap<User, RankingBadgeOwnersItemDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(e => e.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(e => e.Name))
            .ForMember(dest => dest.TotalBadges, opt => opt.MapFrom(e => e.Badges.Count))
            .ReverseMap();

            Mapper.CreateMap<User, RankingCheckinItemDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(e => e.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(e => e.Name))
            .ForMember(dest => dest.TotalCheckins, opt => opt.MapFrom(e => e.ChangeSets.Count))
            .ReverseMap();
        }
    }
}
