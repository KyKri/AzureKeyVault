# Hello!
This is my little dotnet console app for interacting with Azure Key Vault.

I threw this together while training for the AZ 203 certification test. I loosely followed the MS tutorial here: https://docs.microsoft.com/en-us/azure/key-vault/quick-create-net#create-a-resource-group

## To use this code
Fork or download and then create an appsettings.json file in the same directory as Program.cs.
Contents should be as follows:
- For Key Operations:

{
    "myKey": "your key URI (find in Azure Key Vault under the key name and then under version)"
}
- For Secret Operations

{
    "mySecret: "your key URI (find in Azure Key Vault under the key name and then under version)"
}