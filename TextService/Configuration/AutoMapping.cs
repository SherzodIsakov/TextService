using AutoMapper;
using TextService.Entities;

namespace TextService.Configuration
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<TextService.Repositories.Text, TextFile>().ReverseMap();
        }
    }
}
