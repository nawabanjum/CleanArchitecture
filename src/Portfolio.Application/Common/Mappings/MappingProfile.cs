using AutoMapper;
using Portfolio.Application.DTOs;
using Portfolio.Application.Features.Portfolios.Commands.Create;
using Portfolio.Application.Features.Portfolios.Commands.Update;
using Portfolio.Application.Features.Educations.Commands.Create;
using Portfolio.Application.Features.Educations.Commands.Update;
using Portfolio.Application.Features.Experiences.Commands.Create;
using Portfolio.Application.Features.Experiences.Commands.Update;
using Portfolio.Application.Features.Projects.Commands.Create;
using Portfolio.Application.Features.Projects.Commands.Update;
using Portfolio.Application.Features.Skills.Commands.Create;
using Portfolio.Application.Features.Skills.Commands.Update;
using Portfolio.Application.Features.Certifications.Commands.Create;
using Portfolio.Application.Features.Certifications.Commands.Update;
using Portfolio.Application.Features.SocialLinks.Commands.Create;
using Portfolio.Application.Features.SocialLinks.Commands.Update;

namespace Portfolio.Application.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Entity to DTO
        CreateMap<Domain.Entities.Portfolio, PortfolioDto>();
        CreateMap<Domain.Entities.Education, EducationDto>();
        CreateMap<Domain.Entities.Experience, ExperienceDto>();
        CreateMap<Domain.Entities.Project, ProjectDto>();
        CreateMap<Domain.Entities.Skill, SkillDto>();
        CreateMap<Domain.Entities.Certification, CertificationDto>();
        CreateMap<Domain.Entities.SocialLink, SocialLinkDto>();

        // Command to Entity
        CreateMap<CreatePortfolioCommand, Domain.Entities.Portfolio>();
        CreateMap<UpdatePortfolioCommand, Domain.Entities.Portfolio>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<CreateEducationCommand, Domain.Entities.Education>();
        CreateMap<UpdateEducationCommand, Domain.Entities.Education>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<CreateExperienceCommand, Domain.Entities.Experience>();
        CreateMap<UpdateExperienceCommand, Domain.Entities.Experience>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<CreateProjectCommand, Domain.Entities.Project>();
        CreateMap<UpdateProjectCommand, Domain.Entities.Project>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<CreateSkillCommand, Domain.Entities.Skill>();
        CreateMap<UpdateSkillCommand, Domain.Entities.Skill>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<CreateCertificationCommand, Domain.Entities.Certification>();
        CreateMap<UpdateCertificationCommand, Domain.Entities.Certification>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<CreateSocialLinkCommand, Domain.Entities.SocialLink>();
        CreateMap<UpdateSocialLinkCommand, Domain.Entities.SocialLink>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}
