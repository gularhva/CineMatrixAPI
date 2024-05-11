using AutoMapper;
using CineMatrixAPI.Application.DTOs.BookingDTOs;
using CineMatrixAPI.Application.DTOs.BranchDTOs;
using CineMatrixAPI.Application.DTOs.MovieDTOs;
using CineMatrixAPI.Application.DTOs.ReviewDTOs;
using CineMatrixAPI.Application.DTOs.ShowTimeDTOs;
using CineMatrixAPI.Application.DTOs.TicketDTOs;
using CineMatrixAPI.Application.DTOs.UserDTOs;
using CineMatrixAPI.Domain.Entities;
using CineMatrixAPI.Domain.Entities.Identities;
using CineMatrixAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMatrixAPI.Application.Profiles
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<Movie,MovieCreateUpdateDTO>().ReverseMap();
            CreateMap<Movie,MovieGetDTO>().ReverseMap();
            CreateMap<Branch,BranchGetDTO>().ReverseMap();
            CreateMap<Branch, BranchCreateUpdateDTO>().ReverseMap();
            CreateMap<ShowTime, ShowTimeGetDTO>().ReverseMap();
            CreateMap<Ticket, TicketCreateDTO>().ReverseMap();
            CreateMap<Ticket, TicketGetDTO>().ReverseMap();
            CreateMap<Ticket, TicketUpdateDTO>().ReverseMap();
            CreateMap<AppUser, UserGetDTO>().ReverseMap();
            CreateMap<UserUpdateDTO, AppUser>().ReverseMap();
            CreateMap<ReviewCreateDTO, Review>().ReverseMap();
            CreateMap<ReviewGetDTO, Review>().ReverseMap();
            CreateMap<ReviewUpdateDTO, Review>().ReverseMap();
            CreateMap<BookingCreateDTO, Booking>().ReverseMap();
            CreateMap<BookingGetDTO, Booking>().ReverseMap();
        }
    }
}
