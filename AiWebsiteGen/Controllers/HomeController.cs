using AiWebsiteGen.Models;
using AiWebsiteGen.Data;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Python.Runtime;

namespace AiWebsiteGen.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly websiteDbContext _context;

        public HomeController(ILogger<HomeController> logger, websiteDbContext applicationDbContext)
        {
            _logger = logger;
            _context = applicationDbContext;
        }

       public IActionResult Index()
        {
            bool isAuthenticated = User.Identity?.IsAuthenticated ?? false; // Use null conditional and null coalescing operators to handle null case
            List<GenAI> genAIList = _context.GenAIs.ToList();

            if (genAIList == null || genAIList.Count == 0)
            {
                // Handle the case when genAIList is null or empty
                ViewBag.Message = "No GenAI data available."; // You can customize this message
                var viewModel = new GenViewModel
                {
                    IsAuthenticated = isAuthenticated,
                };
                return View(viewModel);
            }
            else
            {
                // Create an instance of the ViewModel and set its properties
                var viewModel = new GenViewModel
                {
                    IsAuthenticated = isAuthenticated,
                    GenAIList = genAIList
                };

                return View(viewModel);
            }
        }
        //GenAiSites razor page controller
        public IActionResult GenAiSites(){
            bool isAuthenticated = User.Identity?.IsAuthenticated ?? false; // Use null conditional and null coalescing operators to handle null case
            List<GenAI> genAIList = _context.GenAIs.ToList();
            genAIList = genAIList.OrderByDescending(genAI => genAI.Like).ToList();
            if (genAIList == null || genAIList.Count == 0)
            {
                // Handle the case when genAIList is null or empty
                ViewBag.Message = "No GenAI data available."; // You can customize this message
                var viewModel = new GenViewModel
                {
                    IsAuthenticated = isAuthenticated,
                };
                return View(viewModel);
            }
            else
            {
                // Create an instance of the ViewModel and set its properties
                var viewModel = new GenViewModel
                {
                    IsAuthenticated = isAuthenticated,
                    GenAIList = genAIList
                };

                return View(viewModel);
            }
            
        }

        //GenAI
        public IActionResult GenAI()
        {
            return View();
        }
      
        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Jobs()
        {
            try
            {
            Runtime.PythonDLL = @"C:\Users\REYHAN\AppData\Local\Programs\Python\Python310\python310.dll";
            PythonEngine.Initialize();
                using (Py.GIL())
                {
                    //import the python script
                    dynamic script = Py.Import("generate_data");

                    // Call the Python script function to generate data visualizations
                    dynamic result = script.InvokeMethod("generate_data_visualizations");

                    // Extract the table and chart HTML from the result
                    string tableHtml = result["table_html"].ToString();

                    string chartHtml = result["chart_html_bar"].ToString();

                    string pieHtml = result["chart_html_pie"].ToString();

                    // Pass the HTML content to the view
                    ViewBag.TableHtml = tableHtml;
                    ViewBag.ChartHtml = chartHtml;
                    ViewBag.PieHtml = pieHtml;
                }
            }
            catch (InvalidOperationException)
            {
                
            }

            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}