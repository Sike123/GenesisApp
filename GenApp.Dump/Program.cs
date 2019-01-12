using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace GenApp.Dump
{
    public class Program
    {
        private static AutoResetEvent _autoResetEvent;
        private static ManualResetEvent _manualResetEvent;
     

        public static void Main(string[] args)
        {


           var test= Solution(Convert.ToString(1234));

        //   var result=  ClimbingLeaderboard(new[] { 100, 100 ,50 ,40, 40, 20, 10 }, new[] { 5 ,25, 50 ,120 });




            Console.WriteLine("Main Method start");
            _autoResetEvent = new AutoResetEvent(false);
            _manualResetEvent= new ManualResetEvent(false);

            Thread t = new Thread(CalculateSum);
                t.Start(new HashSet<int>(){1,1,3});
            
            Console.WriteLine("Press Enter to Set or Unblock the Calculate Sum Method");
            Console.ReadLine();
           
            //Unblock the thread
            _autoResetEvent.Set();


        }


        public static int FormulatToCalculatePermutation(string input)
        {

            var product = 1;
            for (int i = input.Length ; i >0; i--)
            {
               product= product* i;
            }
            int test = input.Length - 2;

            var divisor = 1;
            for (int i = test; i > 0; i--)
            {
                divisor = divisor * i;
            }

            return product / divisor;
        }


        public static int ShortestDistanceBetweenItemsInInteger(int[] numbers)
        {
            var lowestDifference = 0;
            bool isFirstCheck = true;
            for (int i = 0; i < numbers.Length; i++)
            {
                for (int j = i + 1; j < numbers.Length; j++)
                {
                    var num1 = numbers[i];
                    var num2 = numbers[j];
                    var difference = Math.Abs(num1 - num2);
                    Console.WriteLine(difference);
                    if (isFirstCheck)
                    {

                      
                        lowestDifference = difference;
                        isFirstCheck = false;
                    }
                    else
                    {
                        if (lowestDifference > difference)

                            lowestDifference = difference;
                    }
                }
            }
            return lowestDifference;
        }




        public static void CalculateSum(object nums)
        {
            Console.WriteLine("Calculate Sum Method Started and summing the input");

            Dictionary<int,string> s= new Dictionary<int, string>();
            var sum = ((HashSet<int>)nums).Sum();

            Console.WriteLine("Calculate Method now blocked ");

            //block this thread
            _autoResetEvent.WaitOne();

            Console.WriteLine($"Calculate Sum Method Unblocked. Sum is {sum} Press Enter to Exit");
           
        }


        static int[] ClimbingLeaderboard(int[] scores, int[] alice)
        {

            var nonDuplicateScores = scores.Distinct().ToArray();

          
            var result = new List<int>();
            for (int i = 0; i < alice.Length; i++)
            {
                for (int j = 0;j< nonDuplicateScores.Length;j++)
                {
                    var aliceVal = alice[i];

                    var nonDupsVal = nonDuplicateScores[j];

                    if  (alice[i]>nonDuplicateScores[j] )
                    {
                        result.Add(j+1);
                        break;
                    }

                    if (nonDuplicateScores[j] == alice[i])
                    {
                        result.Add(j+1);
                        break;
                    }

                    if (j + 1 == nonDuplicateScores.Length)
                    {
                        result.Add(nonDuplicateScores.Length+1);
                    }
                   
                }
             
            }

            return result.ToArray();
        }


    }

}
