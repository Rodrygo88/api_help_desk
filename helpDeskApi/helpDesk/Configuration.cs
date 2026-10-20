namespace helpDesk
{
    public static class Configuration
    {
        public static string JwtKey =>
            Environment.GetEnvironmentVariable("HELPDESK_JWT_KEY")
            ?? throw new InvalidOperationException("Configure a variável de ambiente HELPDESK_JWT_KEY.");
    }
}
