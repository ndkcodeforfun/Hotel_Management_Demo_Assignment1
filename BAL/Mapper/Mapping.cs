using AutoMapper;
using BAL.ModelView;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Mapper
{
    public class Mapping : Profile
    {
        public Mapping() {
            CreateMap<Customer, CustomerView>().ReverseMap();
            CreateMap<RoomType, RoomTypeView>().ReverseMap();   
            CreateMap<RoomInformation, RoomInformationView>().ReverseMap();
            CreateMap<Customer, RegisterInfoView>().ReverseMap();
        }
    }
}
