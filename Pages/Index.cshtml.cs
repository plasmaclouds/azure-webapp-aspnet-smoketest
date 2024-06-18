using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Numerics;

namespace Azure_Web_App_Test.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private static BigInteger PerformHeavyComputation()
        {
            int n = 1000; // number of iterations
            BigInteger a = 0;
            BigInteger b = 1;
            for (int i = 0; i < n; i++)
            {
                BigInteger temp = a;
                a = b;
                b = temp + b;
            }
            return a;
        }

        public int RandomNumber { get; private set; }
        public BigInteger ComputationResult { get; private set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            RandomNumber = new Random().Next(1, 100); //generates a random number between 1 and 100
            ComputationResult = PerformHeavyComputation(); // Perform heavy computation
        }
    }
}
