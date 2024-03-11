using System;

namespace ConsoleGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(60, 32); // Set console window size

            int playerX = 0; // Initial x position of the player
            int playerY = 0; // Initial y position of the player

            Random rnd = new Random();
            int npcX = rnd.Next(60); // Random initial x position of the NPC
            int npcY = rnd.Next(30); // Random initial y position of the NPC

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
                            Console.Write("O"); // Draw the NPC
                        }
                        else
                        {
                            Console.Write("."); // Draw empty space
                        }
                    }
                    Console.WriteLine();
                }

                // Prompt the user for input
                Console.WriteLine("Enter move (e.g., 5S, 3A, 7D, 4W) + Enter: ");
                string command = Console.ReadLine().ToUpper(); // Read the command and convert to uppercase

                // Extract the direction and steps from the command
                char direction = command[command.Length - 1];
                int steps = int.Parse(command.Substring(0, command.Length - 1));

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

                // The console will clear and redraw the board at the start of the next loop iteration
            }
        }
    }
}
