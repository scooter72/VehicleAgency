using System;
using System.Text;
using System.Linq;

namespace VehicleAgency
{
    /// <summary>
    /// Console window mneu utility class
    /// </summary>
    internal class ConsoleMenu
    {
        /// <summary>
        /// Prints the the label and enum names to the console
        /// </summary>
        /// <param name="enumType"></param>
        /// <param name="label"></param>
        internal static void PrintMenu(Type enumType, string label)
        {
            string[] optionsNames = Enum.GetNames(enumType);
            Console.WriteLine("--");
            Console.WriteLine(label);
            Array.ForEach(optionsNames, i => Console.WriteLine($"{i} - {i}"));
            Console.WriteLine("Hit Backspace key to go back to previous menu.");
        }

        /// <summary>
        /// Reads input from the console enables the user to cancel using Backspace key.
        /// The input gets also validated using the min max arguments.
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        internal static int GetUserSelection(int min, int max)
        {
            ConsoleKeyInfo cki;

            StringBuilder str = new StringBuilder();
            while((cki = Console.ReadKey()).Key != ConsoleKey.Enter)
            {
                if (cki.Key == ConsoleKey.Backspace)
                {
                    return -1;
                }
                else if (!(char.IsDigit(cki.KeyChar)))
                {

                    Console.WriteLine("not a digit!");
                    continue;
                }
                else 
                {
                    var input = int.Parse(str.ToString() + cki.KeyChar.ToString());
                    if (input < min|| input > max)
                    {
                        Console.WriteLine("value not in range!");
                        continue;
                    }
                }

                str.Append(cki.KeyChar.ToString());
            } 

            return str.ToString().Length > 0 ? int.Parse(str.ToString()) : -1;
        }
    }
}
