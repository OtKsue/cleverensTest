using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cleverensTest
{
    internal class Server
    {
        private static int count;
        private static readonly ReaderWriterLockSlim rwLock = new ReaderWriterLockSlim();

        public static int GetCount()
        {
            rwLock.EnterReadLock();

            try
            {
                Console.WriteLine(
                    $"{DateTime.Now:HH:mm:ss.fff} READ START T{Environment.CurrentManagedThreadId}");

                Thread.Sleep(3000);

                Console.WriteLine(
                    $"{DateTime.Now:HH:mm:ss.fff} READ END   T{Environment.CurrentManagedThreadId}");

                return count;
            }
            finally
            {
                rwLock.ExitReadLock();
            }
        }

        public static void AddCount(int value)
        {
            rwLock.EnterWriteLock();

            try
            {
                Console.WriteLine(
                    $"{DateTime.Now:HH:mm:ss.fff} WRITE START T{Environment.CurrentManagedThreadId}");

                Thread.Sleep(5000);

                count += value;

                Console.WriteLine(
                    $"{DateTime.Now:HH:mm:ss.fff} WRITE END   T{Environment.CurrentManagedThreadId}");
            }
            finally
            {
                rwLock.ExitWriteLock();
            }
        }
    }
}
