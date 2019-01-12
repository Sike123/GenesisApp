using System.Threading;

namespace GenApp.Dump
{
    public class GetSumOfOneMillionNo
    {
        private static double sum = 0;
        public static double GetSum()
        {
         

            /*
            for (double i = 0; i < 100; i++)
            {
                sum = sum + i;
                Thread.Sleep(3000);

            }*/

            var threadStart = new ParameterizedThreadStart(GetPartialSums);
            Thread t= new Thread(threadStart);

            t.Start();
            t.Join();

            return sum;
        }

        public static void  GetPartialSums(object start)
        {
            int sum = 0;

            for (int i = 0; i < 100; i++)
            {
                sum = sum + i;
            }
        }


    }
}
