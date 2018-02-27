using MazeSolverApi.Models;
using MazeSolverApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MazeSolverApi.Controllers
{
    [Route("solveMaze")]
    public class MazeSolverController : Controller
    {
        private readonly ISolveMazeService _mazeService;

        public MazeSolverController(ISolveMazeService mazeService)
        {
            _mazeService = mazeService;
        }

        [HttpPost]
        public async Task<MazeResult> Create()
        {
            MemoryStream ms = new MemoryStream();
            await Request.Body.CopyToAsync(ms);
            byte[] msArray = ms.ToArray();
            string maze = Encoding.UTF8.GetString(msArray, 0, msArray.Length);

            return _mazeService.SolveMaze(maze);
        }
    }
}
