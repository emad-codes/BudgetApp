using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Budget.API.Domain.Models;
using Budget.API.Domain.Models.Queries;
using Budget.API.Domain.Services;
using Budget.API.Resources;

namespace Budget.API.Controllers
{
    [Route("/api/budgetlevel")]
    [Produces("application/json")]
    [ApiController]
    public class BudgetLevelController : Controller
    {
        private readonly IBudgetLevelService _budgetLevelService;
        private readonly IMapper _mapper;

        public BudgetLevelController(IBudgetLevelService budgetLevelService, IMapper mapper)
        {
            _budgetLevelService = budgetLevelService;
            _mapper = mapper;
        }

        /// <summary>
        /// Lists all existing budget levels.
        /// </summary>
        /// <returns>List of budget level entries.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(QueryResultResource<BudgetLevelResource>), 200)]
        public async Task<QueryResultResource<BudgetLevelResource>> ListAsync([FromQuery] BudgetLevelQueryResource query)
        {
            var productsQuery = _mapper.Map<BudgetLevelQueryResource, BudgetLevelQuery>(query);
            var queryResult = await _budgetLevelService.ListAsync(productsQuery);

            var resource = _mapper.Map<QueryResult<BudgetLevel>, QueryResultResource<BudgetLevelResource>>(queryResult);
            return resource;
        }

        /// <summary>
        /// Saves a new budget level.
        /// </summary>
        /// <param name="resource">Budget level data.</param>
        /// <returns>Response for the request.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(BudgetLevelResource), 201)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> PostAsync([FromBody] SaveBudgetLevelResource resource)
        {
            var product = _mapper.Map<SaveBudgetLevelResource, BudgetLevel>(resource);
            var result = await _budgetLevelService.SaveAsync(product);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            var BudgetLevelResource = _mapper.Map<BudgetLevel, BudgetLevelResource>(result.Resource);
            return Ok(BudgetLevelResource);
        }

        /// <summary>
        /// Updates an existing budget level according to an identifier.
        /// </summary>
        /// <param name="id">Budget level identifier.</param>
        /// <param name="resource">Budget level data.</param>
        /// <returns>Response for the request.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(BudgetLevelResource), 201)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveBudgetLevelResource resource)
        {
            var product = _mapper.Map<SaveBudgetLevelResource, BudgetLevel>(resource);
            var result = await _budgetLevelService.UpdateAsync(id, product);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            var BudgetLevelResource = _mapper.Map<BudgetLevel, BudgetLevelResource>(result.Resource);
            return Ok(BudgetLevelResource);
        }

        /// <summary>
        /// Deletes a given Budget level according to an identifier.
        /// </summary>
        /// <param name="id">Budget level identifier.</param>
        /// <returns>Response for the request.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(BudgetLevelResource), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _budgetLevelService.DeleteAsync(id);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            var categoryResource = _mapper.Map<BudgetLevel, BudgetLevelResource>(result.Resource);
            return Ok(categoryResource);
        }
    }
}