using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using RelicsAPI.Data.DTOs.Categories;
using RelicsAPI.Data.DTOs.Orders;
using RelicsAPI.Data.DTOs.Relics;
using RelicsAPI.Data.Entities;

namespace RelicsAPI
{
    public class RelicsProfile: Profile
    {
        public RelicsProfile()
        {
            CreateMap<Category, CategoryDTO>();
            CreateMap<CreateCategoryDTO, Category>();
            CreateMap<UpdateCategoryDTO, Category>();

            CreateMap<Relic, RelicDTO>();
            CreateMap<CreateRelicDTO, Relic>();
            CreateMap<UpdateRelicDTO, Relic>();

            CreateMap<Order, OrderDTO>();
            CreateMap<CreateOrderDTO, Order>();
            CreateMap<UpdateOrderDTO, Order>();
        }
    }
}
