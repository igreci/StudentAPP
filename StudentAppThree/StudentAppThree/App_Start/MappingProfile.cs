using StudentAppThree.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace StudentAppThree.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Studenti, StudentViewModel>();
            CreateMap<StudentViewModel, Studenti>();
        }
    }
}