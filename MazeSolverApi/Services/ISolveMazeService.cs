using MazeSolverApi.Models;

namespace MazeSolverApi.Services
{
    public interface ISolveMazeService
    {
        MazeResult SolveMaze(string maze);
    }
}
