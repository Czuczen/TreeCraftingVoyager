namespace TreeCraftingVoyager.Server.Services.EmailSenderService
{
    public class EmailSenderOptions
    {
        public string Server { get; set; }
        public int Port { get; set; }
        public string EmailFrom { get; set; }
        public string EmailFromName { get; set; }
        public string EmailAddInfo { get; set; }
    }
}
