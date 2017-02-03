using AutoMapper;
using innovation4austria.data;
using innovation4austria.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace innovation4austria.web
{
    public static class AutoMapperConfig
    {
        public static IMapper CommonMapper;
        public static void RegisterMappings()
        {
            var commonConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<CommonProfile>());
            CommonMapper = commonConfiguration.CreateMapper();
        }
    }

    public class CommonProfile : Profile
    {
        protected override void Configure()
        {
            base.CreateMap<User, ProfileDataModel>();
        }
    }


    public static class AutoMapperExtension
    {
        /// extension method for easy use of ignore syntax
        //Mapper.CreateMap<JsonRecord, DatabaseRecord>()
        //.Ignore(record => record.Field)
        //.Ignore(record => record.AnotherField)
        //.Ignore(record => record.Etc);
        public static IMappingExpression<TSource, TDestination> Ignore<TSource, TDestination>(this IMappingExpression<TSource, TDestination> map, Expression<Func<TDestination, object>> selector)
        {
            map.ForMember(selector, config => config.Ignore());
            return map;
        }
    }
}