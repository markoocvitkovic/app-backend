using AutoMapper;
using AstraLicenceManager.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Security;

namespace AstraLicenceManager.Dto
{
     public class UserDto
    {

        [Key]
        public long Id { get; set; }

        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        [Required]
        public string Email { get; set; }
     
        public string Password { get; set; }

        public Boolean IsActive { get; set; }

        public DateTime InsertDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public long InsertUserId { get; set; }

        public long? UpdateUserId { get; set; }
    }

    public class UserDtoProfile : Profile
    {
        public UserDtoProfile()
        {
            CreateMap<UserDto, User>().ReverseMap();
        }
    }
}
