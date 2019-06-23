using System;

namespace RandomWalkDungeonGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialize 
            RandomWalkMap randomMap = new RandomWalkMap();
            Random randomNumber = new Random();

            // TO DO: Stop this infinite loop when Esc key pressed.
            while (true)
            {
                /*
                 * Generate a random dungeon map with a MaxTunnelLenght of between 7 & 15 cells and a required tunnel ammount 
                 * of between 70 & 300. It will generate a new map everytime the enter key is pressed.
                */

                randomMap.MaxTunnelLength = randomNumber.Next(7, 15);
                randomMap.TunnelsRequired = randomNumber.Next(70, 300);

                // Generate a new random map and print to console.
                randomMap.GenerateRectangleMap();

                // Wait
                Console.ReadLine();
            }
        }
    }
}
