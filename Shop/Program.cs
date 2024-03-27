using System;

namespace ConsoleGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(60, 32); // Set console window size

            while (true)
            {
                // Start a new game session
                RunGameSession(false);
                
                // Ask if the player wants to play again
                Console.WriteLine("Do you want to play again? (Y/N)");
                string input = Console.ReadLine().ToUpper();
                if (input != "Y")
                    break;
            }
        }

        static void RunGameSession(bool isSecondGame)
        {
            int playerX = 0; // Initial x position of the player
            int playerY = 0; // Initial y position of the player

            Random rnd = new Random();
            int npcX = rnd.Next(60); // Random initial x position of the NPC
            int npcY = rnd.Next(30); // Random initial y position of the NPC

            // Character to represent NPC
            char npcCharacter = isSecondGame ? 'G' : 'O';

            // Character to represent obstacle
            char obstacleCharacter = isSecondGame ? 'T' : 'G';
            
            // List to hold obstacle positions
            List<(int, int)> obstaclePositions = new List<(int, int)>();

            // Generate obstacle positions
            if (isSecondGame)
            {
                for (int i = 0; i < 5; i++)
                {
                    int obstacleX = rnd.Next(60);
                    int obstacleY = rnd.Next(30);

                    // Ensure the obstacle is not overlapping with the NPC
                    while ((obstacleX == npcX && obstacleY == npcY) || (obstacleX == npcX + 5 && obstacleY == npcY))
                    {
                        obstacleX = rnd.Next(60);
                        obstacleY = rnd.Next(30);
                    }

                    obstaclePositions.Add((obstacleX, obstacleY));
                }
            }

            while (true)
            {
                Console.Clear(); // Clear the console screen

                // Draw the game board
                for (int y = 0; y < 30; y++)
                {
                    for (int x = 0; x < 60; x++) // Adjusted for wider board
                    {
                        if (x == playerX && y == playerY)
                        {
                            Console.Write("P"); // Draw the player
                        }
                        else if (x == npcX && y == npcY)
                        {
                            Console.Write(npcCharacter); // Draw NPC or finish line
                        }
                        else if (isSecondGame && obstaclePositions.Contains((x, y)))
                        {
                            Console.Write(obstacleCharacter); // Draw obstacle during second game
                        }
                        else
                        {
                            Console.Write("."); // Draw empty space
                        }
                    }
                    Console.WriteLine();
                }

                // Check if the player is touching the NPC
                if (playerX == npcX && playerY == npcY)
                {
                    if (npcCharacter == 'G')
                    {
                        Console.WriteLine("Congratulations! You reached the finish line!");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        return; // Exit the current game session
                    }
                    else
                    {
                        Console.WriteLine("You touched the NPC!");
                        Console.WriteLine("Starting a new game...");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();

                        // Start a new game session
                        RunGameSession(true); // Indicate that it's the second game session
                        return; // Exit the current game session
                    }
                }
                
                // Check if the player is touching the obstacle
                if (isSecondGame && obstaclePositions.Any(pos => pos.Item1 == playerX && pos.Item2 == playerY))
                {
                    Console.WriteLine("You touched an obstacle! You died!");
                    Console.WriteLine("Do you want to start a new game? (Y/N)");
                    string input = Console.ReadLine().ToUpper();
                    if (input == "Y")
                    {
                        // Start a new game session
                        RunGameSession(true); // Indicate that it's the second game session
                    }
                    else
                    {
                        return; // Exit the current game session
                    }
                }

                // Prompt the user for input
                Console.WriteLine("Enter move (e.g., 5S, 3A, 7D, 4W) + Enter: ");
                string command = Console.ReadLine().ToUpper(); // Read the command and convert to uppercase

                // Check if the input length is not equal to 2 or if it's not in the correct format
                if (command.Length != 2 || !char.IsLetter(command[1]) || !char.IsDigit(command[0]))
                {
                    Console.WriteLine("Invalid input format. Please use the format: [Number][Direction] (e.g., 5S)");
                    continue; // Restart the loop
                }

                // Extract the direction and steps from the command
                char direction = command[1];
                int steps = int.Parse(command.Substring(0, 1));

                // Move the player based on user input
                for (int i = 0; i < steps; i++)
                {
                    if (direction == 'A' && playerX > 0)
                    {
                        playerX--;
                    }
                    else if (direction == 'D' && playerX < 59) // Adjusted for wider board
                    {
                        playerX++;
                    }
                    else if (direction == 'W' && playerY > 0)
                    {
                        playerY--;
                    }
                    else if (direction == 'S' && playerY < 29)
                    {
                        playerY++;
                    }
                }
            }
        }
    }
}
