using MazeSolverApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MazeSolverApi.Services
{
    public class SolveMazeService : ISolveMazeService
    {
        private readonly int[][] _possibleMoves = {
            new int[] { 1, 0 }, //move right
            new int[] { -1, 0 }, //move left
            new int[] { 0, 1 }, //move down
            new int[] { 0, -1 } //move up
        };
        private int[] _startingStep;
        private int[] _endStep;
        private int _totalMazeSteps;
        private static bool[,] _visitedPaths;
        private Queue<PathStep> _pathQueue;

        /// <summary>
        /// Solves the maze.
        /// </summary>
        /// <param name="inputMaze">Maze string posted from api</param>
        /// <returns>MazeResult model with the number of steps required to solve the maze and the solved maze string</returns>
        public MazeResult SolveMaze(string inputMaze)
        {
            char[][] maze = CreateMazeArray(inputMaze);
            _visitedPaths = new bool[maze.Length, maze[0].Length];
            _pathQueue = new Queue<PathStep>();
            char[][] solvedMaze = GetMazeSolution(maze);
            string completedMaze = GetMazeString(solvedMaze);
            if(completedMaze.Count(x => x == '@') > 0)
            {
                return new MazeResult { Steps = _totalMazeSteps, Solution = completedMaze };
            }

            return new MazeResult { Steps = 0, Solution = "No solution available." };
        }

        private char[][] GetMazeSolution(char[][] maze)
        {
            _pathQueue.Enqueue(new PathStep { X = _startingStep[0], Y = _startingStep[1] });

            while (_pathQueue.Count > 0)
            {
                PathStep currentStep = _pathQueue.Dequeue();

                if (currentStep.X == _endStep[0] && currentStep.Y == _endStep[1])
                {
                    // returns array that represents the solved maze
                    return GetSolvedMaze(maze, currentStep);
                }

                AddPathSteps(maze, currentStep);
            }

            return maze;
        }

        private void AddPathSteps(char[][] maze, PathStep currentStep)
        {
            // checks for all possible moves of the currentStep
            foreach (var move in _possibleMoves)
            {
                var nextX = currentStep.X + move[0];
                var nextY = currentStep.Y + move[1];
                if (IsValidPathStep(maze, currentStep.Y, nextX, nextY))
                {
                    // adds the next step to _pathQueue while retaining the currentStep as part of the next step and set as visited
                    var nextStep = new PathStep { X = nextX, Y = nextY, PreviousStep = currentStep };
                    _pathQueue.Enqueue(nextStep);
                    _visitedPaths[nextY, nextX] = true;
                }
            }
        }

        private char[][] GetSolvedMaze(char[][] maze, PathStep currentStep) {
            _totalMazeSteps = 0;

            // loops through all previous steps totaling all maze steps
            // and adding @ symbol to designate the completed path through the maze.
            do
            {
                _totalMazeSteps++;
                currentStep = currentStep.PreviousStep;
                if(maze[currentStep.Y][currentStep.X] != 'A') maze[currentStep.Y][currentStep.X] = '@';
            } while (currentStep.PreviousStep != null);

            return maze;
        }

        private char[][] CreateMazeArray(string inputMaze)
        {
            string[] mazeLines = inputMaze.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            char[][] maze = new char[mazeLines.Length][];

            // loops through maze lines to build maze array and set starting and end steps
            for (var i = 0; i < mazeLines.Length; i++)
            {
                maze[i] = mazeLines[i].ToCharArray();

                if (mazeLines[i].Contains("A"))
                {
                    _startingStep = new int[] { Array.FindIndex(maze[i], m => m == 'A'), i };
                }

                if (mazeLines[i].Contains("B"))
                {
                    _endStep = new int[] { Array.FindIndex(maze[i], m => m == 'B'), i };
                }
            }

            return maze;
        }

        private bool IsValidPathStep(char[][] maze, int currentY, int nextX, int nextY)
        {
            // checks criteria to determine if path step is valid.
            return !((nextX < 0) || (nextY < 0) ||
                (nextY >= maze.Length) ||
                (nextX >= maze[currentY].Length) ||
                (maze[nextY][nextX] == '#') ||
                (_visitedPaths[nextY, nextX]));
        }

        private string GetMazeString(char[][] maze)
        {
            // loops through the maze array to build string of the completed maze.
            StringBuilder sb = new StringBuilder();
            for (var i = 0; i < maze.Length; i++)
            {
                sb.Append(new String(maze[i]));
                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}
