using AutoMapper;
using TastifyAPI.DTOs;
using TastifyAPI.DTOs.CreateDTOs;
using TastifyAPI.DTOs.UpdateDTOs;
using TastifyAPI.Entities;

namespace TastifyAPI.Mapping
{
    public class RestaurantProfile : Profile
    {
        public RestaurantProfile()
        {
            CreateMap<Restaurant, RestaurantDTO>();
            CreateMap<RestaurantCreateDTO, Restaurant>();
            CreateMap<RestaurantUpdateDTO, Restaurant>();
        }
    }
}