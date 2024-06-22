namespace TreeCraftingVoyager.Server.Constants;

public static class ClientApps
{
    public const string ClientApp1Dev = "https://localhost:5173";
    public const string ClientApp2Dev = "https://localhost:7265";
    public const string ClientApp3Dev = "http://localhost:5157";
    public const string ClientApp4Dev = "http://localhost:13555";
        
        
    public const string ClientApp1Prod = "https://yourfrontendurl1.com";
    public const string ClientApp2Prod = "https://yourfrontendurl2.com";


    public static readonly string[] OriginsListProd = 
    {
        //ClientApp1,
        //ClientApp2,
    };


    public static readonly string[] OriginsListDev =
    {
        ClientApp1Dev,
        ClientApp2Dev,
        ClientApp3Dev,
        ClientApp4Dev
    };
}