using System.Linq;
using System.Text.RegularExpressions;

namespace VATCheckUsingVIES
{
    static class CheckCountriesVATRequirements
    {
        //Check VAT for DK, FI, HU, LU, MT, SI
        public static bool IsNumbersOkDigits8(string numberToCheck)
        {
            if (numberToCheck.Length != 8) return false;
            else if (float.TryParse(numberToCheck, out var k) != true) return false;

            return true;
        }

        //Check VAT for EE, DE, EL, PT, UK, LT
        public static bool IsNumbersOkDigits9(string numberToCheck)
        {
            if (numberToCheck.Length != 9) return false;
            else if (float.TryParse(numberToCheck, out var k) != true) return false;

            return true;
        }

        //Check VAT for PL, RO, SK, LT
        public static bool IsNumbersOkDigits10(string numberToCheck)
        {
            if (numberToCheck.Length != 10) return false;
            else if (float.TryParse(numberToCheck, out var k) != true) return false;

            return true;
        }

        //Check VAT for HR, IT, LV
        public static bool IsNumbersOkDigits11(string numberToCheck)
        {
            if (numberToCheck.Length != 11) return false;
            else if (float.TryParse(numberToCheck, out var k) != true) return false;

            return true;
        }

        //Check VAT for SE, LT
        public static bool IsNumbersOkDigits12(string numberToCheck)
        {
            if (numberToCheck.Length != 12) return false;
            else if (float.TryParse(numberToCheck, out var k) != true) return false;

            return true;
        }

        //Check VAT for BG
        public static bool IsNumbersOkBulgaria(string numberToCheck)
        {
            if (numberToCheck.Length != 9 & numberToCheck.Length != 10) return false;
            else if (float.TryParse(numberToCheck, out var k) != true) return false;

            return true;
        }

        //Check VAT for AT
        public static bool IsNumbersOkAustria(string numberToCheck)
        {
            if (numberToCheck.Length != 9) return false;
            else if (numberToCheck[0] != 'U') return false;
            else if (float.TryParse(numberToCheck.Substring(1, 8), out var k) != true) return false;

            return true;
        }

        //Check VAT for BE
        public static bool IsNumbersOkBelgium(string numberToCheck)
        {
            if (numberToCheck.Length != 10 | numberToCheck.Length != 9) return false;
            else if (numberToCheck.Length == 9) numberToCheck = '0' + numberToCheck;
            else if (float.TryParse(numberToCheck, out var k) != true) return false;

            return true;
        }

        //Check VAT for CY
        public static bool IsNumbersOkCyprus(string numberToCheck)
        {
            if (numberToCheck.Length != 9) return false;
            else if (char.IsLetter(numberToCheck[8]) == false) return false;
            else if (float.TryParse(numberToCheck.Substring(0, 8), out var k) != true) return false;

            return true;
        }

        //Check VAT for CZ
        public static bool IsNumbersOkCzech(string numberToCheck)
        {
            if (numberToCheck.Length < 8 || numberToCheck.Length > 12) return false;
            else if (numberToCheck.Length > 10) numberToCheck = numberToCheck.Remove(0, 3);
            else if (float.TryParse(numberToCheck, out var k) != true) return false;

            return true;
        }

        //Check VAT for ES
        public static bool IsNumbersOkSpain(string numberToCheck)
        {
            if (numberToCheck.Length != 9) return false;
            else if (char.IsLetter(numberToCheck[0]) == false && char.IsLetter(numberToCheck[8]) == false) return false;
            else if (float.TryParse(Regex.Replace(numberToCheck, "[^0-9.]", ""), out var k) != true) return false;

            return true;
        }

        //Check VAT for IE
        public static bool IsNumbersOkIreland(string numberToCheck)
        {
            if (numberToCheck.Length < 8 || numberToCheck.Length > 9) return false;
            else if (char.IsLetter(numberToCheck[numberToCheck.Length - 1]) == false) return false;
            else if (char.IsLetter(numberToCheck[1]) == false && char.IsLetter(numberToCheck[numberToCheck.Length - 1]) == false && char.IsLetter(numberToCheck[numberToCheck.Length - 2]) == false) return false;
            else if (float.TryParse(Regex.Replace(numberToCheck, "[^0-9.]", ""), out var k) != true) return false;

            return true;
        }

        //Check VAT for NL
        public static bool IsNumbersOkNetherlands(string numberToCheck)
        {
            if (numberToCheck.Length != 12) return false;
            else if (numberToCheck[9] != 'B') return false;
            else if (float.TryParse(Regex.Replace(numberToCheck, "[^0-9.]", ""), out var k) != true) return false;

            return true;
        }

        //Check VAT for FR
        public static bool IsNumbersOkFrance(string numberToCheck)
        {
            if (numberToCheck.Length != 11) return false;
            else if (numberToCheck.Contains('O') || numberToCheck.Contains('I')) return false;
            else if (float.TryParse(numberToCheck.Remove(0, 2), out var k) != true) return false;
            else if (char.IsLetter(numberToCheck[0]) == false & char.IsLetter(numberToCheck[1]) == false) return false;

            return true;
        }

        //Check VAT for CHE
        public static bool IsNumbersOkSwitzerland(string numberToCheck)
        {
            string[] _array = { "MWST", "TVA", "IVA" };
            bool inList;
            numberToCheck = numberToCheck.Replace(".", string.Empty).Replace("-", string.Empty);
            var _lastPart = numberToCheck.Substring(9, numberToCheck.Length - 9);
            numberToCheck = Regex.Replace(numberToCheck, "[^0-9.]", "");

            if (numberToCheck.Length != 9) return false;
            else if (float.TryParse(numberToCheck, out var k) != true) return false;
            else if (inList = _array.Contains(_lastPart) != true) return false;

            return true;
        }

        //Check VAT for IS
        public static bool IsNumbersOkIceland(string numberToCheck)
        {
            if (numberToCheck.Length < 5 || numberToCheck.Length > 7) return false;
            else if (float.TryParse(numberToCheck, out var k) != true) return false;

            return true;
        }

        //Check VAT for NO
        public static bool IsNumbersOkNorway(string numberToCheck)
        {
            var _lastPart = numberToCheck.Substring(9, 3);
            numberToCheck = numberToCheck.Remove(9, 3);
            if (numberToCheck.Length != 9) return false;
            else if (float.TryParse(numberToCheck, out var k) != true) return false;
            else if (_lastPart != "MVA") return false;

            return true;
        }
    }
}
