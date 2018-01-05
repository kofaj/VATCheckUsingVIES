using System;
using System.Windows.Forms;

namespace VATCheckUsingVIES
{
    public partial class VatChecker : Form
    {
        private const int MINIMUM_VAT_LENGHT = 8;

        public VatChecker()
        {
            InitializeComponent();
            AutoCompleteTextOnProgramStart();
        }

        //Data loaded to boxes - on start screen
        private void AutoCompleteTextOnProgramStart()
        {
            NipBox.Text = "PL 6342709934";
            CountryCodeBox.Text = "PL";
            VatNumberBox.Text = "6342709934";
            TraderNameBox.Text = "SENETIC SPÓŁKA AKCYJNA";
            TraderCompanyBox.Text = "WAPIAAAAU8ht_dmv";
            TraderStreetBox.Text = "TADEUSZA KOŚCIUSZKI 227";
            TraderPostCodeBox.Text = "40-600";
            TraderCityBox.Text = "KATOWICE";
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
#if Debug
            DialogResult _dr = MessageBox.Show("Napewno chcesz wyjść?", "Exit window", MessageBoxButtons.YesNo);
            if (_dr == DialogResult.Yes)
            {
                Application.ExitThread();
            }
            else e.Cancel = true;
#endif
        }

        #region CheckUserInput
        private void CheckButton_Click(object sender, EventArgs e)
        {
            ResultLabel.ResetText();
            string countryCode = ExtractCountryCodeFromInput();
            string VATCode = ExtractVatCodeFromInput();

            if (VatIsFine() && ValidateVatByCountryCode(countryCode, VATCode))
            {
                CheckVatByVies(countryCode, VATCode);
            }
            else
            {
                ResultLabel.Text = MessageProvider.ErrorMessage();
            }
        }

        //Second button, only to launch CheckVatApprox
        private void CheckButton2_Click(object sender, EventArgs e)
        {
            string countryCode = ExtractCountryCodeFromInput();
            string VATCode = ExtractVatCodeFromInput();

            if (VatIsFine() && ValidateVatByCountryCode(countryCode, VATCode))
            {
                CheckVatApproxByVies();
                // XmlGenerator.GenerateXmlDoc();
            }
            else
            {
                ResultLabel.Text = MessageProvider.ErrorMessage();
            }
        }

        private bool VatIsFine()
        {
            if (NipBox.Text == null) return false;
            else if (NipBox.Text.Length < MINIMUM_VAT_LENGHT) return false;

            return true;
        }
        #endregion CheckUserInput

        #region SplittedUserInputOnCountrycode_VatNumber
        private string ExtractCountryCodeFromInput()
        {
            return GetNormalizedUserInputData().Substring(0, 2);
        }

        private string ExtractVatCodeFromInput()
        {
            string inputData = GetNormalizedUserInputData();
            return inputData.Substring(2, inputData.Length - 2);
        }

        private string GetNormalizedUserInputData()
        {
            return NipBox.Text.Replace(" ", string.Empty).Replace("-", string.Empty).ToUpper();
        }

        private bool ValidateVatByCountryCode(string countryCode, string VATCode)
        {
            if (countryCode == "CH") countryCode = "CHE";

            switch (countryCode)
            {
                case "DK":
                case "FI":
                case "HU":
                case "LU":
                case "MT":
                case "SI":
                    {
                        return CheckCountriesVATRequirements.IsNumbersOkDigits8(VATCode);
                    }
                case "EE":
                case "DE":
                case "EL":
                case "PT":
                case "UK":
                    {
                        if (CheckCountriesVATRequirements.IsNumbersOkDigits9(VATCode)) return true;
                        break;
                    }
                case "PL":
                case "RO":
                case "SK":
                    {
                        if (CheckCountriesVATRequirements.IsNumbersOkDigits10(VATCode)) return true;
                        break;
                    }
                case "HR":
                case "IT":
                case "LV":
                    {
                        if (CheckCountriesVATRequirements.IsNumbersOkDigits11(VATCode)) return true;
                        break;
                    }
                case "SE":
                    {
                        if (CheckCountriesVATRequirements.IsNumbersOkDigits12(VATCode)) return true;
                        break;
                    }
                case "AT":
                    {
                        if (CheckCountriesVATRequirements.IsNumbersOkAustria(VATCode)) return true;
                        break;
                    }
                case "BE":
                    {
                        if (CheckCountriesVATRequirements.IsNumbersOkBelgium(VATCode)) return true;
                        break;
                    }
                case "CY":
                    {
                        if (CheckCountriesVATRequirements.IsNumbersOkCyprus(VATCode)) return true;
                        break;
                    }
                case "CZ":
                    {
                        if (CheckCountriesVATRequirements.IsNumbersOkCzech(VATCode)) return true;
                        break;
                    }
                case "ES":
                    {
                        if (CheckCountriesVATRequirements.IsNumbersOkSpain(VATCode)) return true;
                        break;
                    }
                case "IE":
                    {
                        if (CheckCountriesVATRequirements.IsNumbersOkIreland(VATCode)) return true;
                        break;
                    }
                case "NL":
                    {
                        if (CheckCountriesVATRequirements.IsNumbersOkNetherlands(VATCode)) return true;
                        break;
                    }
                case "LT":
                    {
                        if (CheckCountriesVATRequirements.IsNumbersOkDigits9(VATCode) ||
                            CheckCountriesVATRequirements.IsNumbersOkDigits12(VATCode)) return true;
                        break;
                    }
                case "FR":
                    {
                        if (CheckCountriesVATRequirements.IsNumbersOkFrance(VATCode)) return true;
                        break;
                    }
                case "CHE":
                    {
                        if (CheckCountriesVATRequirements.IsNumbersOkSwitzerland(VATCode)) return true;
                        break;
                    }
                case "IS":
                    {
                        if (CheckCountriesVATRequirements.IsNumbersOkIceland(VATCode)) return true;
                        break;
                    }
                case "NO":
                    {
                        if (CheckCountriesVATRequirements.IsNumbersOkNorway(VATCode)) return true;
                        break;
                    }
                default:
                    MessageProvider.ErrorMessage();
                    break;
            }

            return false;
        }
        #endregion SplittedUserInputOnCountrycode_VatNumber

        #region VatCheckMethods
        private void CheckVatByVies(string countryCode, string VATCode)
        {
            try
            {
                var result = ServicesMethods.CheckVat(countryCode, VATCode);
                MessageProvider.MessageFromServer(result.IsValid, result.Name, result.Address);
            }
            catch (Exception exceptionMessage)
            {
                MessageProvider.Exceptions(exceptionMessage.Message);
            }
        }

        private void CheckVatApproxByVies()
        {
            string countryCode = ExtractCountryCodeFromInput();
            string VATNumber = ExtractVatCodeFromInput();

            try
            {
                var result = ServicesMethods.CheckVatApprox(countryCode, VATNumber, TraderName, TraderCompanyType,
                TraderStreet, TraderPostcode, TraderCity, RequesterCountryCode, RequesterVatNumber, Valid);

                CountryCodeBox.Text = countryCode;
                VatNumberBox.Text = VATNumber;
                TraderNameBox.Text = result.TraderNameMatch.ToString();
                TraderCompanyBox.Text = result.TraderCompanyTypeMatch.ToString();
                TraderStreetBox.Text = result.TraderStreetMatch.ToString();
                TraderPostCodeBox.Text = result.TraderPostCodeMatch.ToString();
                TraderCityBox.Text = result.TradeerCityMatch.ToString();
                ValidTextBox.Text = result.Valid.ToString();

            }
            catch (Exception ex)
            {
                MessageProvider.Exceptions(ex.Message);
            }
        }
        #endregion VatCheckMethods

        #region Properties
        public string TraderName { get { return TraderNameBox.Text; } }
        public string TraderCompanyType { get { return TraderCompanyBox.Text; } }
        public string TraderStreet { get { return TraderStreetBox.Text; } }
        public string TraderPostcode { get { return TraderPostCodeBox.Text; } }
        public string TraderCity { get { return TraderCityBox.Text; } }
        public string RequesterCountryCode { get { return CountryCodeBox.Text; } }
        public string RequesterVatNumber { get { return VatNumberBox.Text; } }
        public bool Valid { get; set; }
        #endregion Properties

    }

}
