using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using HouseLoan.Api.Model.Domain;
using HouseLoan.Api.Repositories;

namespace HouseLoan.UI.Controllers
{
    public class LoanController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly ILoanRepository _loanRepository;

        public LoanController(IHttpClientFactory httpClientFactory, ILoanRepository loanRepository)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7108/");
            _loanRepository = loanRepository;
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
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var amortizations =  await _loanRepository.GetAllLoansAsync();
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
