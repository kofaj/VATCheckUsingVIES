using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace VATCheckUsingVIES
{
    internal static class MessageProvider
    {

        static Dictionary<string, string> _exceptionCodes = new Dictionary<string, string>()
            {
                {"INVALID_INPUT", "The provided CountryCode is invalid or the VAT number is empty" },
                {"GLOBAL_MAX_CONCURRENT_REQ", "Your Request for VAT validation has not been processed" },
                {"MS_MAX_CONCURRENT_REQ", "Your Request for VAT validation has not been processed; the maximum number of concurrent requests for this Member State has been reached" },
                {"SERVICE_UNAVAILABLE","an error was encountered either at the network level or the Web application level" },
                {"MS_UNAVAILABLE", "The application at the Member State is not replying or not available" },
                {"TIMEOUT", "The application did not receive a reply within the allocated time period" }
            };

        public static string ErrorMessage()
        {
            Label label = new Label();
            label.ForeColor = System.Drawing.Color.Red;
            return String.Format("Wprowadzony NIP" + "\n" + "jest niewłaściwy");
        }

        public static void MessageFromServer(bool valid, string name, string address)
        {
            string b = valid == true ? "Tak" : "Nie";
            MessageBox.Show(String.Format("Czy Vat jest aktywny? {0}" + "\n" + "\n" + "Nazwa właściciela: {1}" +
                "\n" + "\n" + "Adres: {2}", b, name, address));
        }

        //Handling exceptions received from server
        public static void Exceptions(string ex)
        {
            if (_exceptionCodes != null && _exceptionCodes.TryGetValue(ex, out string exceptionDescription))
            {
                MessageBox.Show(exceptionDescription, "Error message");
            }
            else MessageBox.Show(ex, "Error message");
        }

    }
}
