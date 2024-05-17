using HouseLoan.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System.Text;
using HouseLoan.Api.Model.Domain;
using Microsoft.EntityFrameworkCore;
using HouseLoan.Api.Data;
using HouseLoan.Api.Repositories;

namespace HouseLoan.UI.Controllers
{
    public class LoanController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly LoanDbContext dbContext;
        private readonly ILoanRepository loanRepository;

        public LoanController(IHttpClientFactory httpClientFactory, ILoanRepository loanRepository)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7108/");
            this.loanRepository = loanRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost] 
        public async Task<IActionResult> Add(LoanParam loanParameters)
        {
            var json = JsonConvert.SerializeObject(loanParameters);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/LoanCalculator/calculate", data);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var loanResult = JsonConvert.DeserializeObject<LoanResult>(content);
            return View();
        }
        public async Task<IActionResult> GetAllLoansAsync()
        {
            var amortizations =  await loanRepository.GetAllLoansAsync();
            return View(amortizations);
        }


        public async Task<IActionResult> GetLoanResult(int loanId)
        {
            var response = await _httpClient.GetAsync($"api/Loan/{loanId}");
                var content = await response.Content.ReadAsStringAsync();
                var loanResult = JsonConvert.DeserializeObject<LoanResult>(content);
            return View(loanResult);
        }

    }
}
