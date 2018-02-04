# StringBenchmark
_Everybody knows_ that string concatenation with a plus sign is a very inefficient way of doing. Using a StringBuilder is much more preferable.

But it turned out that it is not always true. If we are looking for the way of building a cell address in an "Excel-way", the most primitive way shows the best performance.

```
                     Method |     Mean |    Error |    StdDev | Scaled | ScaledSD |
--------------------------- |---------:|---------:|----------:|-------:|---------:|
              'Create cell' | 156.9 ns | 1.167 ns | 1.0912 ns |   1.00 |     0.00 | -- background value
         String.Format(...) | 509.8 ns | 2.292 ns | 2.1442 ns |   3.25 |     0.03 | -- 100%
         String.Concat(...) | 483.8 ns | 2.087 ns | 1.9523 ns |   3.08 |     0.02 | --  92%
   StringBuilder.ToString() | 400.3 ns | 1.099 ns | 0.8580 ns |   2.55 |     0.02 | --  69%
 "$"+col+"$"+row.ToString() | 356.7 ns | 1.615 ns | 1.5107 ns |   2.27 |     0.02 | --  56%
```

Building cell range addresses ("$AB$100:$DE$200"), however, shows a different result. StringBuilder wins:

```
                   Method |     Mean |    Error |   StdDev | Scaled | ScaledSD |
------------------------- |---------:|---------:|---------:|-------:|---------:|
           'Create cells' | 314.0 ns | 2.647 ns | 2.476 ns |   1.00 |     0.00 | -- background value
       String.Format(...) | 958.4 ns | 3.317 ns | 3.103 ns |   3.05 |     0.03 | -- 100%
       String.Concat(...) | 968.8 ns | 2.740 ns | 2.288 ns |   3.08 |     0.02 | -- 101%
 StringBuilder.ToString() | 855.4 ns | 2.593 ns | 2.425 ns |   2.72 |     0.02 | --  84%
              String.Plus | 893.8 ns | 1.883 ns | 1.761 ns |   2.85 |     0.02 | --  90%
```