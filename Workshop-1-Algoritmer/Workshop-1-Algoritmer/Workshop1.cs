using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Workshop_1_Algoritmer.Algorithms;

namespace Workshop_1_Algoritmer
{
    class Workshop1
    {
        private List<LocationData> data; //En liste af type LocationData
        private int[,] grid; //2 dimentionelt array af type int
        private int[] gridArray;
        private string filePath;

        //Double floating precision numbers
        private double xMin;
        private double xMax;
        private double yMin;
        private double yMax;

        private int xInterval;
        private int yInterval;

        private int gridLow;
        private int gridHigh;
        private int gridMedian;
        private int gridAvg;

        public Workshop1() { } // Tom constructor. Bliver kaldt når der laves et objekt af klassen

        public void Run() // Run funktion som kalder CLI
        {
            CLI();
        }
        private void CLI()
        {
            bool isTrue = true;

            Console.WriteLine("Welcome to Workshop 1!");
            Console.WriteLine("Which file would you like to read?");
            Console.WriteLine("Small: Press 1 \n" +
                               "Medium: Press 2 \n" +
                               "Large: Press 3");

            while (isTrue)
            {
                ConsoleKey key = Console.ReadKey().Key; //Et objekt key, af typen ConsoleKey, som assignes til brugerens tast
                Console.WriteLine();

                switch (key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                    case ConsoleKey.Oem1:
                        filePath = Path.GetFullPath(@"../../workshop_data_small.csv");
                        Console.WriteLine("Reading the small file now...");
                        ReadFile(); // Kalder funktion ReadFile, som er længere nede i dokumentet
                        isTrue = false;
                        break;

                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                    case ConsoleKey.Oem2:
                        filePath = Path.GetFullPath(@"../../workshop_data_medium.csv");
                        Console.WriteLine("Reading the medium file now...");
                        ReadFile();
                        isTrue = false;
                        break;

                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                    case ConsoleKey.Oem3:
                        filePath = Path.GetFullPath(@"../../workshop_data_large.csv");
                        Console.WriteLine("Reading the large file now...");
                        ReadFile();
                        isTrue = false;
                        break;

                    default:
                        Console.WriteLine("Invalid character, try again!");
                        break;
                }
            }
            Console.WriteLine();
            Console.WriteLine("How many intervals would you like to plot the data into?");
            Console.WriteLine("If you would like the data to be inserted into an NxN grid, write any integer and press enter");
            Console.WriteLine("If you would like the data to be inserted into an NxM grid, write it as 'NxM' and press enter");
            string input; // laver streng ved navn input, som bruges flere steder

            isTrue = true;
            while (isTrue)
            {
                input = Console.ReadLine();
                Console.WriteLine();

                var isNumber = Int32.TryParse(input, out int value); // Laver en bool, som siger om udtrykket er sandt eller falsk. Prøver at konvatere streng til int.
                string[] entries = input.Split('x'); // Deler strengen op, hvor der er et x. Den tager højre og venstre side af x'et, og gemmer dem i et array.

                if (isNumber) //Hvis det er et nummer, køres dette
                {
                    FindMinMax(); //Kalder FindMinMax, som er længere nede
                    xInterval = value;
                    yInterval = value;
                    Console.WriteLine($"Data will be plotted into a {value}x{value} grid");
                    Console.WriteLine($"Each square is {(xMax - xMin) / xInterval} * {(xMax - xMin) / yInterval}");
                    isTrue = false;

                }
                else if(entries.Length == 2) // Hvis arrayet har 2 elementer, kør dette
                {
                    var nIsNumber = Int32.TryParse(entries[0], out int nValue); // x værdien
                    var mIsNumber = Int32.TryParse(entries[1], out int mValue); // y værdien

                    if (nIsNumber && mIsNumber)
                    {
                        FindMinMax();
                        xInterval = nValue;
                        yInterval = mValue;
                        Console.WriteLine($"Data will be plotted into a {nValue}x{mValue} grid");
                        Console.WriteLine($"Each square is {((xMax - xMin) / xInterval)} * {(xMax - xMin) / yInterval}");
                        isTrue = false;
                    }
                }
                else //ellers kør dette
                {
                    Console.WriteLine("Incorrect input, please try again...");
                }
            }


            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();

            Console.WriteLine();
            Console.WriteLine("Plotting the data...");
            Console.WriteLine();
            ClearArray(); // Kalder ClearArray. Findes længere nede
            PlotData();

            Console.WriteLine();
            Console.WriteLine("Printing the data...");
            Console.WriteLine();
            PrintGrid();
            
            Console.WriteLine();
            PrintGridStats();

            Console.WriteLine();
            Console.WriteLine("Would you like to see a specific region of the grid?");
            Console.WriteLine("y/n");

            isTrue = true;
            while (isTrue)
            {
                input = Console.ReadLine();
                if (input == "y" || input == "Y")
                {
                    Console.WriteLine("Where would you like it to start?");
                    Console.WriteLine("Write it as nxm");
                    var from = Console.ReadLine();
                    Console.WriteLine("Where would you like it to end?");
                    Console.WriteLine("Write it as nxm");
                    var to = Console.ReadLine();
                    isTrue = false;
                    PrintPartialGrid(from, to);
                }
                else if (input == "n" || input == "N")
                {
                    isTrue = false;
                }
                else
                {
                    Console.WriteLine("The input was incorrect.. Try again");
                }
            }
        }
        private void ReadFile()
        {
            ReadFromFile FileReader = new ReadFromFile(filePath); // Laver en ny instans af ReadFromFile, med fil stien som input

            var starttime = DateTime.Now; //Gemmer datoen inden vi kører ReadFile
            data = FileReader.ReadFile(); // Read the file
            var stoptime = DateTime.Now; //Gemmer datoen efter vi har kørt ReadFile

            var intDuration = stoptime - starttime; // Finder differencen af tid
            Console.WriteLine($"Reading the file took: {intDuration}");
        }
        private void FindMinMax()
        {
            xMin = data[0].x;
            xMax = data[0].x;
            yMin = data[0].y;
            yMax = data[0].y;

            for (int i = 0; i < data.Count; i++)
            {
                if (data[i].x < xMin) //Hvis data[i].x er mindre end xMin, så opdater xMin til at være data[i].x
                {
                    xMin = data[i].x;
                }
                if (data[i].x > xMax) //Hvis data[i].x er større end xMax, så opdater xMax til at være data[i].x
                {
                    xMax = data[i].x;
                }

                if (data[i].y < yMin) //Hvis data[i].y er mindre end yMin, så opdater yMin til at være data[i].y
                {
                    yMin = data[i].y;
                }
                if (data[i].y > yMax) //Hvis data[i].y er større end yMax, så opdater yMax til at være data[i].y
                {
                    yMax = data[i].y;
                }
            }
        }
        private void ClearArray()
        {
            grid = new int[xInterval, yInterval];

            for (int i = 0; i < xInterval; i++)
            {
                for (int j = 0; j < yInterval; j++)
                {
                    grid[i, j] = 0; // Sørger for, at der er 0 i alle firkanter
                }
            }
        }
        private void PlotData()
        {
            var starttime = DateTime.Now;
            for (int i = 0; i < data.Count; i++)
            {
                int x = (int)Hash(data[i].x, xMin, xMax, xInterval); //Conveterer returværdien double til en int. Kalder Hash funktionen med parametre
                int y = (int)Hash(data[i].y, yMin, yMax, yInterval); // ---||---

                grid[x, y]++; // Tæller firkanten op med én, hvor det nuværende data punkt passer ind i griddet
            }
            var stoptime = DateTime.Now;

            var intDuration = stoptime - starttime;
            Console.WriteLine($"Plotting the data took: {intDuration}");
        }
        public double Hash(double val, double min, double max, int interval)
        {
            var Scale = (int)(val + Math.Abs(min)) / (max - min);

            var Index = Scale * interval;

            return Index;
        }
        private void ConvertGridToArray()
        {
            gridArray = new int[xInterval * yInterval];
            int counter = 0;
            for (int i = 0; i < xInterval; i++)
            {
                for (int j = 0; j < yInterval; j++)
                {
                    gridArray[counter] = grid[i, j];
                    counter++;
                }
            }
        }
        private void SortGridArray()
        {
            var starttime = DateTime.Now;
            Quick.Sort(gridArray);
            var stoptime = DateTime.Now;

            var intDuration = stoptime - starttime;
            Console.WriteLine($"Sorting the data took: {intDuration}");
        }
        private void PrintGrid()
        {
            for (int i = 0; i < xInterval; i++)
            {
                for (int j = 0; j < yInterval; j++)
                {
                    Console.Write($"({i},{j}) = {grid[i, j],-10} ");
                }
                Console.WriteLine();
            }
        }
        private void PrintGridArray()
        {
            foreach (var item in gridArray)
            {
                Console.Write($"{item} ");
            }
        }
        private void PrintPartialGrid(string from, string to)
        {
            string[] fromArr = from.Split('x');
            string[] toArr = to.Split('x');

            var isFromX = Int32.TryParse(fromArr[0], out int fromX);
            var isFromY = Int32.TryParse(fromArr[1], out int fromY);
            var isToX = Int32.TryParse(toArr[0], out int toX);
            var isToY = Int32.TryParse(toArr[1], out int toY);

            if (isFromX && isFromY && isToX && isToY
                && fromX <= toX && fromY <= toY)
            {
                for (int i = fromX; i < toX + 1; i++)
                {
                    for (int j = fromY; j < toY + 1; j++)
                    {
                        Console.Write($"({i},{j}) = {grid[i, j],-10} ");
                    }
                }
                Console.WriteLine();
            }
        }
        private void PrintGridStats()
        {
            ConvertGridToArray();
            SortGridArray();

            for (int i = 0; i < gridArray.Length; i++)
            {
                if (gridArray[i] > 0)
                {
                    gridLow = gridArray[i];
                    break;
                }
            }

            gridHigh = gridArray[gridArray.Length - 1];

            for (int i = 0; i < gridArray.Length; i++)
            {
                gridAvg += gridArray[i];
            }
            gridAvg /= gridArray.Length;

            gridMedian = gridArray[gridArray.Length / 2];

            Console.WriteLine($"Grid low: {gridLow}");
            Console.WriteLine($"Grid High: {gridHigh}");
            Console.WriteLine($"Grid Median: {gridMedian}");
            Console.WriteLine($"Grid average: {gridAvg}");



        }
    }
}
