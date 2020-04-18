using System;

namespace VehicleAgency
{
    class Util
    {
        internal static int GetNumericInput(int min, int max)
        {
            int value = -1;

            for (int i = 0; i < 3; i++)
            {
                if ((!int.TryParse(Console.ReadLine(), out value))
                    || (value < min)
                    || (value > max))
                {
                    Console.WriteLine($"Invalid value must be within {min}-{max}");
                }
                else
                {
                    break;
                }
            }

            return value;
        }

    }
}
