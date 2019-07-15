using System;
using System.IO;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.KeyVault.Models;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace KeyVault
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder().AddJsonFile("C:/Users/k_r_k/OneDrive/Documents/Programming/AZ203/KeyVault/appsettings.json");
            IConfiguration config = builder.Build();
            // Replace myKeyURI with a URI from your Azure Key Vault
            string myKeyURI = config.GetSection("myKey").Value;

            // Connect using client credentials to access Key Vault
            AzureServiceTokenProvider azureServiceTokenProvider = new AzureServiceTokenProvider();
            KeyVaultClient keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));

            // Get key from Key Vault
            try
            {
                KeyBundle key = keyVaultClient.GetKeyAsync(myKeyURI).GetAwaiter().GetResult();
                // To-Do Actually use a key for something...
                Console.WriteLine(key.Key);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
