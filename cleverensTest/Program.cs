using cleverensTest;

class Program
{
    static void Main(string[] args)
    {


        //Compressor and Decompressor
        /* 
        try
        {
            Console.Write("Введите строку: ");
            string source = Console.ReadLine();

            string compressed = StringCompressor.Compress(source);
            string decompressed = StringCompressor.Decompress(compressed);

            Console.WriteLine($"Сжатая строка: {compressed}");
            Console.WriteLine($"Восстановленная строка: {decompressed}");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
        */



        // Server with ReaderWriterLockSlim
        /*
        var tasks = new List<Task>();

        for (int i = 0; i < 5; i++)
        {
            tasks.Add(Task.Run(() => Server.GetCount()));
        }

        tasks.Add(Task.Run(() => Server.AddCount(10)));

        Task.WaitAll(tasks.ToArray());
        */


    }
}