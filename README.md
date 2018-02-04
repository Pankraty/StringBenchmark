# StringBenchmark
_Everybody knows_ that string concatenation with a plus sign is a very inefficient way of doing. Using a StringBuilder is much more preferable.

But it turned out that it is not always true. If we are looking for the way of building a cell address in an "Excel-way", the most primitive way shows the best performance.

```
                     Method |     Mean |    Error |    StdDev | Scaled |
--------------------------- |---------:|---------:|----------:|-------:|
         String.Format(...) | 509.8 ns | 3.862 ns | 3.6127 ns |   1.00 |
         String.Concat(...) | 477.3 ns | 3.119 ns | 2.9174 ns |   0.94 |
   StringBuilder.ToString() | 399.4 ns | 1.109 ns | 1.0373 ns |   0.78 |
 "$"+col+"$"+row.ToString() | 355.1 ns | 1.028 ns | 0.8582 ns |   0.70 |
```

Building cell range addresses ("$AB$100:$DE$200"), however, shows a different result. StringBuilder wins:

```
                   Method |     Mean |    Error |   StdDev | Scaled |
------------------------- |---------:|---------:|---------:|-------:|
       String.Format(...) | 958.2 ns | 3.888 ns | 3.637 ns |   1.00 |
       String.Concat(...) | 964.7 ns | 6.498 ns | 5.761 ns |   1.01 |
 StringBuilder.ToString() | 852.3 ns | 3.512 ns | 3.114 ns |   0.89 |
              String.Plus | 891.9 ns | 1.813 ns | 1.696 ns |   0.93 |
```