using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SherloqApp.Data;

namespace SherloqApp.Controllers
{
    public class TablesController : Controller
    {
        private ISherloqContext _context;
        public TablesController(ISherloqContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Get()
        {
            var tables = await _context.Tables.ToListAsync();

            return Ok(tables);
        }
    }
}
