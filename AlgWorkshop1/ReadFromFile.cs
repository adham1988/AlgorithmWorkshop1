using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Workshop_1_Algoritmer
{
    class ReadFromFile
    {
        List<LocationData> DataSet = new List<LocationData>(); // List for containing the data
        private string WorkshopFile; // File path of the data

        public ReadFromFile(string file)
        {
            WorkshopFile = file;
        }

        /// <summary>
        /// The function for reading the file
        /// </summary>
        /// <returns> A list of all the data</returns>
        public List<LocationData> ReadFile()
        {
            List<string> lines = File.ReadAllLines(WorkshopFile).ToList(); // Read all the lines seperately, and save them in the list

            //If the list is empty or null, throw an exception
            if (lines == null || lines.Count == 0)
            {
                throw new FileNotFoundException();
            }

            lines.RemoveAt(0); // remove the first element, as it does not contain data

            // Iterate through all the elements in the list
            foreach (var item in lines)
            {
                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US"); // This is used to convert american numbers (100.000,00) to (100000)
                NumberStyles style = NumberStyles.Any;


                string[] entries = item.Split(';'); //Create an array of each line, where the element is split at semicolons, as it is a CSV file
                //Try to parse the first element of the array to an int. If successful, idResult is true, and idNumber gets the integer value of the string
                bool idResult = Int32.TryParse(entries[0], style, culture, out int idNumber);

                //Try to parse the second element of the array to a double. If successful, xResult is true, and xNumber gets the integer value of the string
                bool xResult = Double.TryParse(entries[1], style, culture, out double xNumber);

                //Try to parse the third element of the array to a double. If successful, yResult is true, and yNumber gets the integer value of the string
                bool yResult = Double.TryParse(entries[2], style, culture, out double yNumber);

                // Check that all parses were successful
                if (idResult && xResult && yResult)
                {
                    LocationData data = new LocationData(idNumber, xNumber, yNumber); // Create new object with the data for this location data
                    DataSet.Add(data); // Add it to the list
                }
            }
            return DataSet; // Return the formatted list
        }
    }
}
