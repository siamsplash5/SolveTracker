using AutoMapper;
using Microsoft.AspNetCore.Identity.Data;
using SolveTracker.Domain.Entities.Common;
using SolveTracker.Domain.Entities.DailyLog;
using SolveTracker.Domain.Entities.Registration;
using SolveTracker.Web.Models.DailyLog;
using SolveTracker.Web.Models.Login;
using SolveTracker.Web.Models.Registration;

namespace SolveTracker.Web.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<RegistrationViewModel, RegistrationRequest>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => (UserRole)src.Role));

        CreateMap<LoginViewModel, LoginRequest>();
        CreateMap<DailyLogViewModel, DailyLogInfo>();
    }
}
