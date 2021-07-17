using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MList.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var list = new MList<Test>();
            for (int i = 0; i < 65; i++)
            {
                list.Add(new Test()
                {
                    Id = i,
                    Name = "Jens",
                    Pet = "Søren",
                });
            }
            list.RemoveAll(o => o.Id > 30);
        }
    }
    public class Test
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Pet { get; set; }
    }
}
