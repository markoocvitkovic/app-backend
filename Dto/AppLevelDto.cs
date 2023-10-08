using AstraLicenceManager.Entities;
using AutoMapper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace AstraLicenceManager.Dto
{
    public class AppLevelDto
    {
        [Key]
        public long Id { get; set; }

        public int Value { get; set; }

        public string Description { get; set; }

        [ForeignKey("App")]
        public long AppId { get; set; }

        public App? App { get; set; }

        [ForeignKey("Company")]
        public long CompanyId { get; set; }

        public Company? Company { get; set; }

        public DateTime InsertDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        [ForeignKey("User")]
        public long InsertUserId { get; set; }

        public User? User { get; set; }

        public long? UpdateUserId { get; set; }
    }

    public class AppLevelDtoProfile : Profile
    {
        public AppLevelDtoProfile()
        {
            CreateMap<AppLevelDto, AppLevel>().ForMember(a => a.Company, opt => opt.Ignore())
                                              .ForMember(a => a.App, opt => opt.Ignore())
                                              .ForMember(a => a.User, opt => opt.Ignore());

            CreateMap<AppLevel, AppLevelDto>();
        }
    }
}
