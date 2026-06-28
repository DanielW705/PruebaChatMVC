namespace LibreriaChatMVC.Seeders
{
    public interface ISeeder<T> where T : class
    {
        T[] ApplySeed();
    }

    public interface ISeeder<T, in K> where T : class
    {
        T[] ApplySeed(K seed);
    }
    public interface ISeeder<T, in K, in W> where T : class 
    {
        T[] ApplySeed(K seed1, W seed2);
    }
}
