using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;
using System.Text;

namespace StringBenchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<StringBenchmark>();

            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }
    }

    public class StringBenchmark
    {
        public class Cell
        {
            static Random rnd = new Random();

            readonly string[] columns = {
                "A", "B", "C", "D", "E", "F",
                "AB", "CD", "EF", "GH", "IJ", "KL",
                "ABC", "DEF", "GHI", "JKL", "MNO", "PRQ" };

            public string Col { get; set; }

            public int Row { get; set; }

            public Cell()
            {
                Col = columns[rnd.Next(columns.Length)];
                Row = rnd.Next(1_000_000);
            }
        }
        
        [Benchmark(Description = "String.Format(...)")]
        public string TestFormat()
        {
            var cell = new Cell();
            return Format(cell.Col, cell.Row);
        }

        [Benchmark(Description = "String.Concat(...)")]
        public string TestConcat()
        {
            var cell = new Cell();
            return Concat(cell.Col, cell.Row);
        }

        [Benchmark(Description = "StringBuilder.ToString()")]
        public string TestStringBuilder()
        {
            var cell = new Cell();
            return Builder(cell.Col, cell.Row);
        }

        [Benchmark(Description = "\"$\"+col+\"$\"+row.ToString()")]
        public string TestPlus()
        {
            var cell = new Cell();
            return Plus(cell.Col, cell.Row);
        }

        private string Plus(string col, int row)
        {
            return "$" + col + "$" + row.ToString();
        }
        private string Format(string col, int row)
        {
            return string.Format("${0}${1}", col, row);
        }
        private string Concat(string col, int row)
        {
            return String.Concat("$", col, "$", row);
        }
        private string Builder(string col, int row)
        {
            return new StringBuilder("$").Append(col).Append("$").Append(row).ToString();
        }
    }
}
