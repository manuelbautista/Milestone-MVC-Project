using AutoMapper;
using DCS.Common.DTO;
using DCS.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCS.DAL
{
    public class BaseAutoMap
    {
        public static void Configure()
        {
            Mapper.CreateMap<ETOPlaybook, ETOPlaybookModel>();
            Mapper.CreateMap<Project, ProjectModel>();
            Mapper.CreateMap<ETOPlaybook, ETOPlaybookDTO>();
            Mapper.CreateMap<ProjectStatu, ProjectStatusModel>();
            Mapper.CreateMap<ProjectMilestone, ProjectMilestoneModel>();
            Mapper.CreateMap<ProjectOwner, ProjectOwnerModel>();
            Mapper.CreateMap<ProjectSchedule, ProjectScheduleModel>();
            Mapper.CreateMap<ProjectOwner, ProjectOwnerModel>()
    .ForMember(cv => cv.FullName, m => m.MapFrom(
    s => s.FirstName + " " + s.LastName));

        }
    }
}
