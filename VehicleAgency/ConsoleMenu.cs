using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleAgency
{
    internal class ConsoleMenu
    {
        internal static void PrintMenu(Type enumType, string label)
        {
            string[] optionsNames = Enum.GetNames(enumType);
            Console.WriteLine("--");
            Console.WriteLine(label);
            for (int i = 0; i < optionsNames.Length; i++)
            {
                Console.WriteLine($"{i} - {optionsNames[i]}");
            }

            Console.WriteLine("Hit Backspace key to go back to previous menu.");
        }

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
