namespace GameOfLife.Console.ArgumentParser
{
    public interface IArgumentParser
    {
        string GetFilePathArgument();
        bool GetWrapArgument();
    }
}
