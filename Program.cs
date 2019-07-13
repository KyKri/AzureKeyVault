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

            // Get keys in string format, note -- won't be actual value!
            getKeysAsync(keyVaultClient, yourKeyVault).GetAwaiter().GetResult();
        }

        public static async Task<string> getKeysAsync(KeyVaultClient keyVC, string yourKeyVault)
        {
            //To-Do handle exception if keyVault does not exist
            var keys = await keyVC.GetKeysAsync($"https://{yourKeyVault}.vault.azure.net/");
            Console.WriteLine(keys.ToString());
            return keys.ToString();
        }
    }
}
