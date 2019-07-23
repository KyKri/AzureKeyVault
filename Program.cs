using System;
using System.IO;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.KeyVault.Models;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration;

namespace KeyVault
{
    class Program
    {
        static void Main(string[] args)
        {
            // Get Key URI from appsettings.json
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder().AddJsonFile(Path.GetFullPath(Directory.GetCurrentDirectory()) + "/appsettings.json");
                IConfiguration config = builder.Build();

                // Connect using client credentials to access Key Vault
                AzureServiceTokenProvider azureServiceTokenProvider = new AzureServiceTokenProvider();
                KeyVaultClient keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));

                if (args.Length == 1)
                {
                    if (args[0] == "secret")
                    {
                        getMySecret(config, keyVaultClient);
                    }
                    if (args[0] == "key")
                    {
                        getMyKey(config, keyVaultClient);
                    }
                }
                else
                {
                    Console.WriteLine("You must specify either key or secret!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n-- Did you forget to create the appsettings.json file in the same directory as Program.cs?");
            }
        }

        static void getMyKey(IConfiguration config, KeyVaultClient keyVault)
        {
            // To-Do Make sure keyURI is not null or other possible exceptions
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
            // To-Do Make sure secretURI is not null or other possible exceptions
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
