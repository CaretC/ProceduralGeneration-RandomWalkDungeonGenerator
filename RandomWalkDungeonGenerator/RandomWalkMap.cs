using System;
using System.Collections.Generic;
using System.Text;

namespace RandomWalkDungeonGenerator
{
    class RandomWalkMap
    {
        // TO DO: Write a call that uses the Random Walk algorithm to generate a simple random dungeon
        /* 
        TO DO: It would be cool if there is a 'rectangular' varient and a 'natural' variant. One just the raw
        algorithm output the other that a cell is added around each edge cell to make it appear more rounded.
        */

        // ======= Properties =======

        // Sets the character that prints as Walls. Default █.
        public char WallCharacter { get; set; } = '█';

        // Sets the Wall Color. Default is ConsoleColor.Green.
        public ConsoleColor WallColor { get; set; } = ConsoleColor.Green;

        // Sets the default console color. Default is ConsoleColor.Gray.
        public ConsoleColor DefaultColor { get; set; } = ConsoleColor.Gray;

        // Sets the number of tiles required to be generated. Default is 150.
        public int TunnelsRequired { get; set; } = 150;

        // Sets the maximum length a generated tunnel can be. Default is 10.
        public int MaxTunnelLength { get; set; } = 10;

        // ======= Private Variables =======

        // Saves the width of the console
        private int consoleWitdth = Console.WindowWidth;

        // Saves the height of the console.
        private int consoleHeight = Console.WindowHeight;

        // ======= Classes =======
        Random randomNumber = new Random();

        // ======= Public Methods =======
        
        // Generates a Rectangular type map.
        public void GenerateRectangleMap()
        {
            bool[,] map = generateMap();

            printMap(map);       
        }

        // Generates a Natural type map.
        public void GenerateNaturalMap()
        {
            bool[,] map = generateMap();

            // TO DO: Implement 'smoothing' step to make map more natural
            smoothMap(map);

            printMap(map);
        }
                
        // ======= Private Methods =======

        // Make a map full of 'walls'.
        private bool[,] initializeMap()
        {
            bool[,] initialMap = new bool[consoleWitdth, consoleHeight];

            for (int x = 0; x < consoleWitdth; x++)
            {
                for (int y = 0; y < consoleHeight; y++)
                {
                    initialMap[x, y] = true;
                }
            }

            return initialMap;
        }

        // Return a random position within console area.
        private int[,] randomStartPosition()
        {
            int[,] startPosition = new int[2,1];

            startPosition[0,0] = randomNumber.Next(2, (consoleWitdth - 2));
            startPosition[1,0] = randomNumber.Next(2, (consoleHeight - 2));

            return startPosition;
        }

        // Return a random direction.
        private int[,] chooseRandomDirection()
        {
            int[][,] direction = {
                new int[,] { { 0 }, { -1 } },  // Up
                new int[,] { { 0 }, { 1 } },   // Down
                new int[,] { { -1 }, { 0 } },  // Left
                new int[,] { { 1 }, { 0 } }    // Right
            };

            return direction[randomNumber.Next(0, direction.Length)];
        }

        // Make a tunnel of a random length (between 1 and MaxTunnelLength) in a random direction. Returns the the end position of the tunnel.
        private int[,] makeTunnel(bool[,] map, int[,] currentPosition)
        {
            int[,] direction = chooseRandomDirection();
            int[,] endPosition = currentPosition;
            int randomTunnelLength = randomNumber.Next(1, MaxTunnelLength);

            for (int tunnelCell = 0; tunnelCell < randomTunnelLength; tunnelCell++)
            {
                int[,] newPosition = { { currentPosition[0, 0] + direction[0, 0] }, { currentPosition[1, 0] + direction[1, 0] } };

                if (isValid(map, newPosition))
                {
                    setEmpty(map, newPosition[0, 0], newPosition[1, 0]);

                    endPosition[0, 0] += direction[0, 0];
                    endPosition[1, 0] += direction[1, 0];
                }
            }

            return endPosition;
        }

        // Returns isValid
        private bool isValid(bool[,] map, int[,] position)
        {
            bool valid = false;

            if(position[0,0] >= 2 && position[0,0] <= (consoleWitdth - 2))
            {
                if (position[1,0] >= 2 && position[1,0] <= (consoleHeight - 2))
                {
                    valid = true;
                }
            }

            return valid;
        }

        // Sets designated cell to empty.
        private void setEmpty(bool[,] map, int xPosition, int yPosition)
        {
            map[xPosition, yPosition] = false;
        } 

        // Generates a map as a bool array.
        private bool[,] generateMap()
        {
            bool[,] map = initializeMap();

            int[,] startPosition = randomStartPosition();
            int[,] currentPosition = { { startPosition[0, 0] }, { startPosition[1, 0] } };

            setEmpty(map, startPosition[0, 0], startPosition[1, 0]);

            for (int tunnel = 0; tunnel < TunnelsRequired; tunnel++)
            {
                currentPosition = makeTunnel(map, currentPosition);
            }

            return map;
        }

        // Smooths a rectangle map to make a Natural Style map
        private void smoothMap(bool[,] map)
        {
            
        }

        // Prints map to the console.
        private void printMap(bool[,] map)
        {
            Console.Clear();

            Console.ForegroundColor = WallColor;

            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    if (map[x,y])
                    {
                        Console.SetCursorPosition(x, y);
                        Console.Write(WallCharacter);
                    }
                }
            }

            Console.ForegroundColor = DefaultColor;
        }    
    }
}
