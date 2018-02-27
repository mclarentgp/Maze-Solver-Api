namespace MazeSolverApi.Models
{
    public class PathStep
    {
        public int X { get; set; }
        public int Y { get; set; }
        public PathStep PreviousStep { get; set; }
    }
}
