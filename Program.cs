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
            var yourKeyVault = "vaultName";
            var azureServiceTokenProvider = new AzureServiceTokenProvider();
            var keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));
            getKeysAsync(keyVaultClient, yourKeyVault).GetAwaiter().GetResult();
        }

        public static async Task<string> getKeysAsync(KeyVaultClient keyVC, string yourKeyVault)
        {
            var keys = await keyVC.GetKeysAsync($"https://{yourKeyVault}.vault.azure.net/");
            Console.WriteLine(keys.ToString());
            return keys.ToString();
        }
    }
}
