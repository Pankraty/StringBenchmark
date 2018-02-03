# StringBenchmark
_Everybody knows_ that string concatenation with a plus sign is a very inefficient way of doing. Using a StringBuilder is much more preferable.

But it turned out that it is not always true. If we are looking for the way of building a cell address in an "Excel-way", the most primitive way shows the best performance.

```
                      Method |     Mean |     Error |    StdDev |
 --------------------------- |---------:|----------:|----------:|
          String.Format(...) | 510.3 ns | 4.3134 ns | 3.8237 ns |
          String.Concat(...) | 477.5 ns | 0.4184 ns | 0.3913 ns |
    StringBuilder.ToString() | 400.2 ns | 1.0409 ns | 0.9227 ns |
  "$"+col+"$"+row.ToString() | 357.3 ns | 1.3935 ns | 1.0076 ns |
```