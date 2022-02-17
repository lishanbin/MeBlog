using AutoMapper;
using MeBlog.Model;
using MeBlog.Model.DTO;

namespace MeBlog.WebApi.Utility._AutoMapper
{
    public class CustomAutoMapperProfile:Profile
    {
        public CustomAutoMapperProfile()
        {
            base.CreateMap<WriterInfo,WriterInfoDTO>();

            base.CreateMap<BlogNews, BlogNewsDTO>()
                .ForMember(dest => dest.TypeName, source => source.MapFrom(src => src.TypeInfo.Name))
                .ForMember(dest => dest.WriterName,source=>source.MapFrom(src=>src.WriterInfo.Name));

            base.CreateMap<Student, StudentDTO>();
            base.CreateMap<Course, CourseDTO>();
            base.CreateMap<Score, ScoreDTO>()
                .ForMember(dest => dest.StuName, source => source.MapFrom(src => src.Student.Name))
                .ForMember(dest => dest.IDCard, source => source.MapFrom(src => src.Student.IDCard))
                .ForMember(dest => dest.Sex, source => source.MapFrom(src => src.Student.Sex))
                .ForMember(dest => dest.Role, source => source.MapFrom(src => src.Student.Role))
                .ForMember(dest => dest.CourseName, source => source.MapFrom(src => src.Course.CourseName));

        }
    }
}
