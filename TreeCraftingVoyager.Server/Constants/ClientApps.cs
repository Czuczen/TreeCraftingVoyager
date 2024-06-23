namespace TreeCraftingVoyager.Server.Constants;

public static class ClientApps
{
    public const string ClientApp1Dev = "https://localhost:5173";
        
    public const string ClientApp1Prod = "https://yourfrontendurl1.com";


    public static readonly string[] OriginsListProd = 
    {
        ClientApp1Prod,
    };


    public static readonly string[] OriginsListDev =
    {
        ClientApp1Dev
    };
}
