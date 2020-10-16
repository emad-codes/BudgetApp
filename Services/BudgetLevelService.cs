using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Budget.API.Domain.Models;
using Budget.API.Domain.Models.Queries;
using Budget.API.Domain.Repositories;
using Budget.API.Domain.Services;
using Budget.API.Domain.Services.Communication;
using Budget.API.Infrastructure;

namespace Budget.API.Services
{
    public class BudgetLevelService : IBudgetLevelService
    {
        private readonly IBudgetLevelRepository _budgetLevelRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _cache;

        public BudgetLevelService(IBudgetLevelRepository budgetLevelRepository, IUserRepository userRepository, IUnitOfWork unitOfWork, IMemoryCache cache)
        {
            _budgetLevelRepository = budgetLevelRepository;
            _userRepository= userRepository;
            _unitOfWork = unitOfWork;
            _cache = cache;
        }

        public async Task<QueryResult<BudgetLevel>> ListAsync(BudgetLevelQuery query)
        {
            // Here I list the query result from cache if they exist, but now the data can vary according to the category ID, page and amount of
            // items per page. I have to compose a cache to avoid returning wrong data.
            string cacheKey = GetCacheKeyForBudgetLevelQuery(query);
            
            var products = await _cache.GetOrCreateAsync(cacheKey, (entry) => {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
                return _budgetLevelRepository.ListAsync(query);
            });

            return products;
        }

        public async Task<BudgetLevelResponse> SaveAsync(BudgetLevel budgetLevel)
        {
            try
            {
                /*
                 Notice here we have to check if the user ID is valid before adding the product, to avoid errors.
                 You can create a method into the UserService class to return the user and inject the service here if you prefer, but 
                 it doesn't matter given the API scope.
                */
                var existingCategory = await _userRepository.FindByIdAsync(budgetLevel.UserId);
                if (existingCategory == null)
                    return new BudgetLevelResponse("Invalid budget level.");

                await _budgetLevelRepository.AddAsync(budgetLevel);
                await _unitOfWork.CompleteAsync();

                return new BudgetLevelResponse(budgetLevel);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new BudgetLevelResponse($"An error occurred when saving the budget Level: {ex.Message}");
            }
        }

        public async Task<BudgetLevelResponse> UpdateAsync(int id, BudgetLevel budgetLevel)
        {
            var existingBudgetLevel = await _budgetLevelRepository.FindByIdAsync(id);

            if (existingBudgetLevel == null)
                return new BudgetLevelResponse("Budget level not found.");

            var existingCategory = await _userRepository.FindByIdAsync(budgetLevel.UserId);
            if (existingCategory == null)
                return new BudgetLevelResponse("Invalid user.");

            existingBudgetLevel.Level0 = budgetLevel.Level0;
            existingBudgetLevel.Level1 = budgetLevel.Level1;
            existingBudgetLevel.Level2 = budgetLevel.Level2;
            existingBudgetLevel.Level3 = budgetLevel.Level3;
            existingBudgetLevel.Level4 = budgetLevel.Level4;
            existingBudgetLevel.Level5 = budgetLevel.Level5;
            existingBudgetLevel.Status = budgetLevel.Status;

            existingBudgetLevel.Year = budgetLevel.Year;
            existingBudgetLevel.ModifiedAt = budgetLevel.ModifiedAt;
            existingBudgetLevel.CreatedAt = budgetLevel.CreatedAt;

            existingBudgetLevel.UserId = budgetLevel.UserId;
            existingBudgetLevel.ManagerId = budgetLevel.ManagerId;
            existingBudgetLevel.ApproverId = budgetLevel.ApproverId;

            try
            {
                _budgetLevelRepository.Update(existingBudgetLevel);
                await _unitOfWork.CompleteAsync();

                return new BudgetLevelResponse(existingBudgetLevel);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new BudgetLevelResponse($"An error occurred when updating the product: {ex.Message}");
            }
        }

        public async Task<BudgetLevelResponse> DeleteAsync(int id)
        {
            var existingBudgetLevel = await _budgetLevelRepository.FindByIdAsync(id);

            if (existingBudgetLevel == null)
                return new BudgetLevelResponse("Budget Level not found.");

            try
            {
                _budgetLevelRepository.Remove(existingBudgetLevel);
                await _unitOfWork.CompleteAsync();

                return new BudgetLevelResponse(existingBudgetLevel);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new BudgetLevelResponse($"An error occurred when deleting the budget level: {ex.Message}");
            }
        }

        private string GetCacheKeyForBudgetLevelQuery(BudgetLevelQuery query)
        {
            string key = CacheKeys.BudgetLevelsList.ToString();
            
            if (query.UserId.HasValue && query.UserId > 0)
            {
                key = string.Concat(key, "_", query.UserId.Value);
            }

            key = string.Concat(key, "_", query.Page, "_", query.ItemsPerPage);
            return key;
        }
    }
}