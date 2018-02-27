# Maze Solver
This program is an ASP.Net Core WebAPI that accepts a maze input and returns a JSON object with the completed maze and the shortest total amount of steps required to finish the maze.

### Prerequisites
- Visual Studio 2017
- .Net Core 2.0 SDK
- Postman

### Installation & Usage
1. Download the Maze-Solver-Api repository.
2. Open the MazeSolverApi solution file in Visual Studio.
3. Hit F5 to start debugging the application.
4. In Postman in the new request tab change HTTP method from GET to POST.
5. Add the following as your request url http://localhost:8080/solveMaze
6. Select Body then select raw.  Create a maze or paste the provided maze into the text field.  Make sure in the dropdown that the type is set to Text or Text(text/plain).
```
##########
#A...#...#
#.#.##.#.#
#.#.##.#.#
#.#....#B#
#.#.##.#.#
#....#...#
##########
```
You can also select binary under the Body tab and click Choose Files to select one of the maze text files included with the repository.
(Note the following if you would like to build your own maze)
- \# represents a wall.
- A represents your starting point.
- B represents your ending point.
- . represents an open path.
7. Click the Send button.
8. Your result will return as a JSON object.
example: the following is the result that is returned for the maze provided earlier in this document.
```
{
    
  "steps": 14,

  "solution": "##########\r\n#A@@.#...#\r\n#.#@##.#.#\r\n#.#@##.#.#\r\n#.#@@@@#B#\r\n#.#.##@#@#\r\n#....#@@@#\r\n##########\r\n"

}
```
