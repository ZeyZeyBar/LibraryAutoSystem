using LibraryAutoSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Data;

namespace LibraryAutoSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly LibraryContext _context;
        public HomeController(LibraryContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> FunctionsCallsAsync()
        {
            //using (IDbConnection connection = new NpgsqlConnection("LibraryContext"))
            //{
            //    var parameters = new Yazar();
            //    parameters.Add("searchtext", _context.Yazars);

            //    return connection.Query<Yazar>("SearchQuestion", parameters, commandType: CommandType.StoredProcedure);
            //}
            return View();
        }
    }
}
