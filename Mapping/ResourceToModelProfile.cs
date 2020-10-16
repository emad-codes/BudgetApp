using AutoMapper;
using Budget.API.Domain.Models;
using Budget.API.Domain.Models.Queries;
using Budget.API.Resources;

namespace Budget.API.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SaveUserResource, User>();

            CreateMap<SaveBudgetLevelResource, BudgetLevel>();

            CreateMap<BudgetLevelQueryResource, BudgetLevelQuery>();
        }
    }
}