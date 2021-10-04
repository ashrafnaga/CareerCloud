using System;
using System.Threading;

namespace CareerCloud.gRPCTest
{
    class Program
    {
        static void Main(string[] args)
        {
            TestServices _testServices = new TestServices();

            //Thread.Sleep(10000);
            _testServices.StartTest();

            Console.ReadKey();
        }
    }
}
