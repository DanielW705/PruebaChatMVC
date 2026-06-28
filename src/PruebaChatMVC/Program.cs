namespace PruebaChatMVC
{
    public partial class Program
    {
        public static IHost CreateHost(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
            .Build();
        public static async Task Main(string[] args)
        {
            await CreateHost(args).RunAsync();
        }
    }
}