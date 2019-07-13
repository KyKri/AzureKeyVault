using System;
using System.Threading.Tasks;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;

namespace KeyVault
{
    class Program
    {
        static void Main(string[] args)
        {
            // Replace vaultName with the name of your Azure Key Vault
            var yourKeyVault = "vaultName";

            // Connect using client credentials to access Key Vault
            var azureServiceTokenProvider = new AzureServiceTokenProvider();
            var keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));

            // Get keys from KeyVault -- note returns <IPage<KeyItem>>
            // To-Do catch exception for bad key vault URL
            keyVaultClient.GetKeysAsync($"https://{yourKeyVault}.vault.azure.net/").GetAwaiter().GetResult();

            // To-Do Actually use a key for something...
        }
    }
}
