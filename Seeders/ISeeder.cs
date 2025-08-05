namespace PruebaChatMVC.Seeders
{
    public interface ISeeder<T> where T : class
    {
        T ApplySeed();
    }

    public interface ISeeder<T, K> where T : class where K : class
    {
        T ApplySeed(K seed);
    }
}
