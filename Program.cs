using System;
using System.Threading.Tasks;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.KeyVault.Models;
using Microsoft.Azure.Services.AppAuthentication;

namespace KeyVault
{
    class Program
    {
        static void Main(string[] args)
        {
            // Replace myKeyURI with a URI from your Azure Key Vault
            string myKeyURI = "";

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
