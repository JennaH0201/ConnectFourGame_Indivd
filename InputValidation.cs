using System;

namespace ConnectFour
{
    public class InputValidation
    {
        // Function that user enter an integer min~max number
        public static int GetValidatedInteger(string prompt, int min, int max)
        {
            int result;
            Console.Write(prompt);
            string? input = Console.ReadLine();

            while (!int.TryParse(input, out result) || result < min || result > max)
            {
                Console.Write("Invalid input. Please enter the correct number");
                input = Console.ReadLine();
            }

            return result;
        }

        // Calculate cols from rows
        public static int ComputeColumnsFromRows(int rows)
        {
            return rows + 1;
        }

        public static (string discType, int column) ParseInput(string input, int maxColumns)
        {
            if (string.IsNullOrWhiteSpace(input) || input.Length < 2)
            {
                throw new ArgumentException("Invalid input. Please enter the correct number like o5");
            }

            char typeChar = char.ToLower(input[0]);
            string columnPart = input.Substring(1);

            if (typeChar != 'o' && typeChar != 'b')
            {
                throw new ArgumentException("Invalid disc type. Try again");
            }

            if (!int.TryParse(columnPart, out int column) || column < 1 || column > maxColumns)
            {
                throw new ArgumentException("Invalid column number.");
            }

            return (typeChar.ToString(), column - 1);
        }
    }
}