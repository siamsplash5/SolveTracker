using AutoMapper;
using SolveTracker.Models.Common;
using SolveTracker.Models.DailyLog;
using SolveTracker.Models.Login;
using SolveTracker.Models.Registration;
using SolveTracker.ViewModels.DailyLog;
using SolveTracker.ViewModels.Login;
using SolveTracker.ViewModels.Registration;

namespace SolveTracker.Mappings
{
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
}
