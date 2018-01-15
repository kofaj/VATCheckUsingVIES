using System.Windows.Forms;
using VATCheckUsingVIES.myService;

namespace VATCheckUsingVIES
{
    static class ServicesMethods
    {
        public static CheckVatResult CheckVat(string cc, string vatNum)
        {
            var client = new checkVatPortTypeClient();

            client.checkVat(ref cc, ref vatNum, out bool valid, out string name, out string address);
            return new CheckVatResult(cc, vatNum, valid, name, address);
        }

        public static ApproxResult CheckVatApprox(string countryCode, string vatNumber, string traderName, string traderCompanyType,
            string traderStreet, string traderPostcode, string traderCity, string requesterCountryCode, string requesterVatNumber, bool valid)
        {

            matchCode traderNameMatch;
            matchCode traderCompanyTypeMatch;
            matchCode traderStreetMatch;
            matchCode traderPostcodeMatch;
            matchCode traderCityMatch;

            var connection = new checkVatPortTypeClient();

            connection.checkVatApprox(ref countryCode, ref vatNumber, ref traderName, ref traderCompanyType, ref traderStreet, ref traderPostcode,
                ref traderCity, requesterCountryCode, requesterVatNumber, out valid, out string traderAddress, out traderNameMatch, out traderCompanyTypeMatch,
                out traderStreetMatch, out traderPostcodeMatch, out traderCityMatch, out string requestIdentifier);

            return new ApproxResult(valid, traderAddress, traderNameMatch, traderCompanyTypeMatch, traderStreetMatch, traderPostcodeMatch, traderCityMatch, requestIdentifier);
        }

        //Save to xml file => do poprawy
        public static void SaveWindow()
        {
            SaveFileDialog SaveFileDialog1 = new SaveFileDialog();
            SaveFileDialog1.InitialDirectory = @"C:\";
            SaveFileDialog1.RestoreDirectory = true;
            SaveFileDialog1.CheckFileExists = true;
            SaveFileDialog1.Title = "Browse Text Files";
            SaveFileDialog1.Filter = "xml files (*.xml)|*.xml|txt files (*.txt)|*.txt|All files (*.*)|*.*";
            SaveFileDialog1.ShowDialog();
        }
    }

    class CheckVatResult
    {
        public CheckVatResult(string countryCode, string vatNumber, bool isValid, string name, string address)
        {
            CountryCode = countryCode;
            VatNumber = vatNumber;
            IsValid = isValid;
            Name = name;
            Address = address;
        }

        public string CountryCode { get; private set; }
        public string VatNumber { get; private set; }
        public bool IsValid { get; private set; }
        public string Name { get; private set; }
        public string Address { get; private set; }
    }

    class ApproxResult
    {
        public ApproxResult(bool valid, string traderAddress, matchCode traderNameMatch, matchCode traderCompanyTypeMatch,
            matchCode traderStreetMatch, matchCode traderPostCodeMatch, matchCode tradeerCityMatch, string requestIdentifier)
        {
            Valid = valid;
            TraderAddress = traderAddress;
            TraderNameMatch = traderNameMatch;
            TraderCompanyTypeMatch = traderCompanyTypeMatch;
            TraderStreetMatch = traderStreetMatch;
            TraderPostCodeMatch = traderPostCodeMatch;
            TradeerCityMatch = tradeerCityMatch;
            RequestIdentifier = requestIdentifier;
        }

        public bool Valid { get; set; }
        public string TraderAddress { get; set; }
        public myService.matchCode TraderNameMatch { get; set; }
        public myService.matchCode TraderCompanyTypeMatch { get; set; }
        public myService.matchCode TraderStreetMatch { get; set; }
        public myService.matchCode TraderPostCodeMatch { get; set; }
        public myService.matchCode TradeerCityMatch { get; set; }
        public string RequestIdentifier { get; set; }
    }
}
