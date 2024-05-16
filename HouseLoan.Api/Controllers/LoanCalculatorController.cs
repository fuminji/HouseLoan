using HouseLoan.Api.Model.Domain;
using HouseLoan.Api.Repositories;
using HouseLoan.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HouseLoan.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanCalculatorController : ControllerBase
    {
        private readonly ILoanCalculationService loanCalculationService;
        private readonly ILoanRepository loanRepository;

        public LoanCalculatorController(ILoanCalculationService loanCalculationService, ILoanRepository loanRepository)
        {
            this.loanCalculationService = loanCalculationService;
            this.loanRepository = loanRepository;
        }

        [HttpPost("calculate")]
        public async Task<IActionResult> CalculateLoan(LoanParam loanParameters)
        {
            var loanResult = loanCalculationService.CalculateLoan(loanParameters);
            await loanRepository.SaveLoanResultAsync(loanResult);
            return Ok(loanResult);
        }

        [HttpGet("{loanId}")]
        public async Task<IActionResult> GetLoanResult(int loanId)
        {
            var loanResult = await loanRepository.GetLoanResultAsync(loanId);
            return Ok(loanResult);
        }
    }
}
