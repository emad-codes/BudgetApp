using AutoMapper;
using Budget.API.Domain.Models;
using Budget.API.Domain.Models.Queries;
using Budget.API.Resources;

namespace Budget.API.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<User, UserResource>();
            CreateMap<BudgetLevel, BudgetLevelResource>();
            CreateMap<QueryResult<BudgetLevel>, QueryResultResource<BudgetLevelResource>>();
        }
    }
}