using AutoMapper;
using PauliTicket.Application.Features.Categories.Commands.CreateCategory;
using PauliTicket.Application.Features.DTOs.Category;
using PauliTicket.Application.Features.DTOs.Event;
using PauliTicket.Application.Features.DTOs.Order;
using PauliTicket.Application.Features.Events.Commands.CreateEvent;
using PauliTicket.Application.Features.Events.Commands.DeleteEvent;
using PauliTicket.Application.Features.Events.Commands.UpdateEvent;
using PauliTicket.Application.Features.Orders.Commands.CreateOrder;
using PauliTicket.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PauliTicket.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Event, EventListDTO>().ReverseMap();
            CreateMap<Event, CreateEventCommand>().ReverseMap();
            CreateMap<Event, UpdateEventCommand>().ReverseMap();
            CreateMap<Event, EventDetailDTO>().ReverseMap();
            CreateMap<Event, CategoryEventDTO>().ReverseMap();
            CreateMap<Event, EventExportDTO>().ReverseMap();

            CreateMap<Category, CategoryDTO>();
            CreateMap<Category, CategoryListDTO>();
            CreateMap<Category, CategoryEventListDTO>();
            CreateMap<Category, CreateCategoryCommand>();
            CreateMap<Category, CategoryCreationDTO>();

            CreateMap<Order, OrdersForMonthDTO>();
            CreateMap<Order, CreateOrderCommand>().ReverseMap();
            CreateMap<Order, OrderDetailDTO>().ReverseMap();

        }
    }
}
