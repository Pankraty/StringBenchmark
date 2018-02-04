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
            BenchmarkRunner.Run<CellBenchmark>();
            BenchmarkRunner.Run<RangeBenchmark>();

            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }
    }

    public class CellBenchmark
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
        
        [Benchmark(Description = "String.Format(...)", Baseline = true)]
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


    public class RangeBenchmark
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

        [Benchmark(Description = "String.Format(...)", Baseline = true)]
        public string TestFormat()
        {
            var cell1 = new Cell();
            var cell2 = new Cell();
            return Format(cell1.Col, cell1.Row, cell2.Col, cell2.Row);
        }

        [Benchmark(Description = "String.Concat(...)")]
        public string TestConcat()
        {
            var cell1 = new Cell();
            var cell2 = new Cell();
            return Concat(cell1.Col, cell1.Row, cell2.Col, cell2.Row);
        }

        [Benchmark(Description = "StringBuilder.ToString()")]
        public string TestStringBuilder()
        {
            var cell1 = new Cell();
            var cell2 = new Cell();
            return Builder(cell1.Col, cell1.Row, cell2.Col, cell2.Row);
        }

        [Benchmark(Description = "String.Plus")]
        public string TestPlus()
        {
            var cell1 = new Cell();
            var cell2 = new Cell();
            return Plus(cell1.Col, cell1.Row, cell2.Col, cell2.Row);
        }

        private string Plus(string col1, int row1, string col2, int row2)
        {
            return "$" + col1 + "$" + row1.ToString() + ":" + "$" + col2 + "$" + row2;
        }
        private string Format(string col1, int row1, string col2, int row2)
        {
            return string.Format("${0}${1}:${2}${3}", col1, row1, col2, row2);
        }
        private string Concat(string col1, int row1, string col2, int row2)
        {
            return String.Concat("$", col1, "$", row1, ":", "$", col2, "$", row2);
        }
        private string Builder(string col1, int row1, string col2, int row2)
        {
            return new StringBuilder("$").Append(col1).Append("$").Append(row1).Append(":")
                .Append("$").Append(col2).Append("$").Append(row2)
                .ToString();
        }
    }
}
