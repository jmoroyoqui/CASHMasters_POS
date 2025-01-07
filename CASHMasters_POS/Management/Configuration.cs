using CASHMasters_POS.Misc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CASHMasters_POS.Management
{
    public class Configuration
    {
        private readonly string configFilePath;
        private const string FILENAME = "Configuration.json";

        private Currency Currency;
        ConsoleInputValidation validation = new ConsoleInputValidation();

        public Currency TheCurrency
        {
            get { return Currency; }
            set { Currency = value; }
        }
        public Configuration()
        {
            configFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FILENAME);
            FindOrCreateJSON();
            GetSelectedCurrency();
        }

        /// <summary>
        /// Get the currency if JSON configuration is already configurated, 
        /// otherwise allows to select one
        /// </summary>
        private void GetSelectedCurrency()
        {
            var json = JsonConvert.DeserializeObject<Currencies>(File.ReadAllText(configFilePath));
            if (!string.IsNullOrEmpty(json.Currency))
            {
                Currency = new Currency()
                {
                    CurrencyCode = json.Currency,
                    Amounts = json.CurrenciesList[json.Currency]
                };
                return;
            }
            SelectCurrency(json);
        }

        /// <summary>
        /// Select one of the curriencies availables on JSON configuration and updated it in order to keep as preferencies
        /// </summary>
        /// <param name="currencies">Currency json object list</param>
        private void SelectCurrency(Currencies currencies)
        {
            Console.WriteLine("**** SELECT A CURRENCY ****");
            for (int i = 0; i < currencies.CurrenciesList.Count; i++)
            {
                Console.WriteLine($"{(i + 1)}) {currencies.CurrenciesList.ElementAt(i).Key}");
            }
            Console.Write("Your choose: ");
            int option = validation.ConvertToInt(Console.ReadLine());

            if (option <= 0 || option > currencies.CurrenciesList.Count)
            {
                Console.WriteLine("Invalid value, the system will set the currency as USD by default");
                option = 1;
            }

            Currency = new Currency()
            {
                CurrencyCode = currencies.CurrenciesList.ElementAt(option - 1).Key,
                Amounts = currencies.CurrenciesList.ElementAt(option - 1).Value
            };

            UpdateJSONFile(Currency.CurrencyCode);
        }

        /// <summary>
        /// Look for the JSON Configuration file, if not exists it creates a new one with default values.
        /// </summary>
        private void FindOrCreateJSON()
        {
            if (File.Exists(configFilePath)) return;

            var jsonObject = new
            {
                Currency = "",
                Currencies = new
                {
                    USD = new List<decimal>() { 0.01m, 0.05m, 0.10m, 0.25m, 0.50m, 1.00m, 2.00m, 5.00m, 10.00m, 20.00m, 50.00m, 100.00m },
                    MXN = new List<decimal>() { 0.05m, 0.10m, 0.20m, 0.50m, 1.00m, 2.00m, 5.00m, 10.00m, 20.00m, 50.00m, 100.00m }
                }
            };

            WriteToFile(jsonObject);
        }

        /// <summary>
        /// Update the JSON configuration file with the currency selected by user.
        /// </summary>
        /// <param name="currency">Currency code name</param>
        private void UpdateJSONFile(string currency)
        {
            var json = JsonConvert.DeserializeObject<Currencies>(File.ReadAllText(configFilePath));
            json.Currency = currency;
            WriteToFile(json);
        }

        /// <summary>
        /// Create or overwrite a file.
        /// </summary>
        /// <param name="obj">Json to serialize</param>
        private void WriteToFile(object obj)
        {
            string content = JsonConvert.SerializeObject(obj);
            System.IO.File.WriteAllText(configFilePath, content);
        }

        /// <summary>
        /// Struct to allocate JSON configuration object
        /// </summary>
        private struct Currencies
        {
            [JsonProperty("Currency")]
            public string Currency { get; set; }
            [JsonProperty("Currencies")]
            public Dictionary<string, List<decimal>> CurrenciesList { get; set; }
        }
    }
}
