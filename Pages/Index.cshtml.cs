using Microsoft.AspNetCore.Mvc;             // Using directive for ASP.NET Core MVC functionality
using Microsoft.AspNetCore.Mvc.RazorPages;  // Using directive for Razor Pages functionality
using Microsoft.Extensions.Logging;         // Using directive for logging functionality
using System;                               // Using directive for base .NET functionalities like Random
using System.Collections.Generic;           // Using directive for generic collections like List
using System.Numerics;                      // Using directive for BigInteger type
using System.Threading.Tasks;               // Using directive for Task Parallel Library (TPL)

namespace Azure_Web_App_Test.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger; // Logger instance for logging purposes

        // Method to perform a heavy computation (calculates a large Fibonacci number)
        private static BigInteger PerformHeavyComputation()
        {
            int n = 16384;               // Number of iterations for Fibonacci calculation
            BigInteger a = 0;           // First Fibonacci number
            BigInteger b = 1;           // Second Fibonacci number
            for (int i = 0; i < n; i++) // Loop n times to calculate the nth Fibonacci number
            {
                BigInteger temp = a;    // Temporary variable to hold the value of a
                a = b;                  // Move b to a
                b = temp + b;           // Calculate the next Fibonacci number
            }
            return a;                   // Return the nth Fibonacci number
        }

        // Property to store a random number
        public int RandomNumber { get; private set; }
        // public BigInteger ComputationResult { get; private set; } <- old version, threaded stuff below:

        // List to store results of the computations
        public List<BigInteger> ComputationResults { get; private set; } = new List<BigInteger>();

   
        public IndexModel(ILogger<IndexModel> logger) => _logger = logger; // Assign the injected logger to the private field

        // here the multithreading fun starts
        // Asynchronous method that runs when the page is accessed via HTTP GET

        public async Task OnGetAsync()
        {
            var tasks = new List<Task<BigInteger>>();
            for (int i = 0; i < 8; i++) //spawn threads: Loop 8 times to create 8 tasks
            {
                // Add a new task to the list that runs the PerformHeavyComputation method on a separate thread
                tasks.Add(Task.Run(() => PerformHeavyComputation()));
            }
            
            // Wait for all tasks to complete and store the results in ComputationResults
            ComputationResults = (await Task.WhenAll(tasks)).ToList();
        }
    }
}
