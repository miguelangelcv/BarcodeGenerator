using System;

namespace BarcodeGenerator.Services
{
    public class BarcodeService : IBarcodeService
    {
        private const char CENTER_GUARD = '|';
        private const string NEW_BARCODE_PREFIX = "8799999";

        private char GetFirstFlag(char c)
        {
            switch (c)
            {
                case '0': { return '!'; }
                case '1': { return '"'; }
                case '2': { return '#'; }
                case '3': { return '$'; }
                case '4': { return '%'; }
                case '5': { return '&'; }
                case '6': { return '\''; }
                case '7': { return '('; }
                case '8': { return ')'; }
                case '9': { return '*'; }
                default: throw new ArgumentException("Invalid character '" + c + "'");
            }
        }

        private char GetSecondFlag(char c)
        {
            switch (c)
            {
                case '0': { return '`'; }
                case '1': { return 'a'; }
                case '2': { return 'b'; }
                case '3': { return 'c'; }
                case '4': { return 'd'; }
                case '5': { return 'e'; }
                case '6': { return 'f'; }
                case '7': { return 'g'; }
                case '8': { return 'h'; }
                case '9': { return 'i'; }
                default: throw new ArgumentException("Invalid character '" + c + "'");
            }
        }

        private char GetLeftHandA(char c)
        {
            switch (c)
            {
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9': { return c; }
                default: throw new ArgumentException("Invalid character '" + c + "'");
            }
        }

        private char GetLeftHandB(char c)
        {
            switch (c)
            {
                case '0': { return '@'; }
                case '1': { return 'A'; }
                case '2': { return 'B'; }
                case '3': { return 'C'; }
                case '4': { return 'D'; }
                case '5': { return 'E'; }
                case '6': { return 'F'; }
                case '7': { return 'G'; }
                case '8': { return 'H'; }
                case '9': { return 'I'; }
                default: throw new ArgumentException("Invalid character '" + c + "'");
            }
        }

        private char GetRightHand(char c)
        {
            switch (c)
            {
                case '0': { return 'P'; }
                case '1': { return 'Q'; }
                case '2': { return 'R'; }
                case '3': { return 'S'; }
                case '4': { return 'T'; }
                case '5': { return 'U'; }
                case '6': { return 'V'; }
                case '7': { return 'W'; }
                case '8': { return 'X'; }
                case '9': { return 'Y'; }
                default: throw new ArgumentException("Invalid character '" + c + "'");
            }
        }

        private char GetCheck(char c)
        {
            switch (c)
            {
                case '0': { return 'p'; }
                case '1': { return 'q'; }
                case '2': { return 'r'; }
                case '3': { return 's'; }
                case '4': { return 't'; }
                case '5': { return 'u'; }
                case '6': { return 'v'; }
                case '7': { return 'w'; }
                case '8': { return 'x'; }
                case '9': { return 'y'; }
                default: throw new ArgumentException("Invalid character '" + c + "'");
            }
        }

        public int CalcCheckDigit(string code)
        {
            int sum = 0;

            // Calculate the checksum digit here.
            for (int i = code.Length; i > 0; i--)
            {
                int digit = Convert.ToInt32(code.Substring(i - 1, 1));

                if (i % 2 == 0)
                    sum += digit * 3; // Odd
                else
                    sum += digit * 1; // Even
            }
            int checkSum = (10 - (sum % 10)) % 10;

            return checkSum;
        }

        public string GenerateNewBarcode(int code)
        {
            string barcode = NEW_BARCODE_PREFIX;
            barcode += string.Format("{0:D2}", code);
            barcode += CalcCheckDigit(barcode);

            return GenerateBarcode(barcode);
        }

        public string GenerateBarcode(string code)
        {
            string barcode = "";

            barcode += GetFirstFlag(code[0]);  // Pos 1
            barcode += GetSecondFlag(code[1]); // Pos 2
            barcode += GetLeftHandB(code[2]);  // Pos 3
            barcode += GetLeftHandB(code[3]);  // Pos 4
            barcode += GetLeftHandA(code[4]);  // Pos 5
            barcode += GetLeftHandA(code[5]);  // Pos 6
            barcode += GetLeftHandB(code[6]);  // Pos 7
            barcode += CENTER_GUARD; // Center
            barcode += GetRightHand(code[7]);  // Pos 8
            barcode += GetRightHand(code[8]);  // Pos 9
            barcode += GetRightHand(code[9]);  // Pos 10
            barcode += GetRightHand(code[10]); // Pos 11
            barcode += GetRightHand(code[11]); // Pos 12
            barcode += GetCheck(code[12]);     // Pos 13

            return barcode;
        }
    }
}