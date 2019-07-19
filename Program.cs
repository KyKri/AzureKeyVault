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
            // Get Key URI from appsettings.json
            IConfigurationBuilder builder = new ConfigurationBuilder().AddJsonFile(Path.GetFullPath(Directory.GetCurrentDirectory()) + "/appsettings.json");
            IConfiguration config = builder.Build();

            // Connect using client credentials to access Key Vault
            AzureServiceTokenProvider azureServiceTokenProvider = new AzureServiceTokenProvider();
            KeyVaultClient keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));

            //getMyKey(config, keyVaultClient);
            getMySecret(config, keyVaultClient);
        }

        static void getMyKey(IConfiguration config, KeyVaultClient keyVault)
        {
            string myKeyURI = config.GetSection("myKey").Value;

            // Get key from Key Vault
            try
            {
                KeyBundle key = keyVault.GetKeyAsync(myKeyURI).GetAwaiter().GetResult();
                // To-Do Actually use a key for something...
                Console.WriteLine(key.Key);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void getMySecret(IConfiguration config, KeyVaultClient keyVault)
        {
            string mySecretURI = config.GetSection("mySecret").Value;

            try{
                SecretBundle secret = keyVault.GetSecretAsync(mySecretURI).GetAwaiter().GetResult();
                Console.WriteLine(secret.Value);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
