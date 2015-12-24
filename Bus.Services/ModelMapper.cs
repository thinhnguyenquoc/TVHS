using AutoMapper;
using TVHS.Entities;
using TVHS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVHS.Services
{
    public static class ModelMapper
    {
        private static bool mappingConfigured = false;

        public static void Configure()
        {
            // Used to create maps for whole viewmodels
            if (mappingConfigured)
            {
                return;
            }
            //From View --> Entity :Person
            Mapper.CreateMap<Level, ViewModelLevel>();
            Mapper.CreateMap<ViewModelLevel, Level>();
            Mapper.CreateMap<Program, ViewModelProgram>();
            Mapper.CreateMap<ViewModelProgram, Program>();
            Mapper.CreateMap<Sale, ViewModelSale>();
            Mapper.CreateMap<ViewModelSale, Sale>();
            Mapper.CreateMap<Schedule, ViewModelSchedule>();
            Mapper.CreateMap<ViewModelSchedule, Schedule>();
            Mapper.CreateMap<TimeSetting, ViewModelTimeSetting>();
            Mapper.CreateMap<ViewModelTimeSetting, TimeSetting>();
            Mapper.CreateMap<Product, ViewModelProduct>();
            Mapper.CreateMap<ViewModelProduct, Product>();
            Mapper.CreateMap<TimeFrame, ViewModelTimeFrame>();
            Mapper.CreateMap<ViewModelTimeFrame, TimeFrame>();
            Mapper.CreateMap<ViewModelCycle, Cycle>();
            Mapper.CreateMap<Cycle, ViewModelCycle>();
            Mapper.CreateMap<Category, ViewModelCategory>();
            Mapper.CreateMap<ViewModelCategory, Category>();
            mappingConfigured = true;
        }

        public static void Reset()
        {
            Mapper.Reset();
            mappingConfigured = false;
        }
    }
}
