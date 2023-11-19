# AoC

Stevo's Advent of Code Repo.

See Benchmark.Net metrics [here](benchmarks.md).

See answers and low-res timings [here](results.md).

# Visualisations

~~Now supports cross-platform visualisations.~~

Works on macOS. There is an issue with Windows at the moment involving FreeImage.
Will get that resolved when I can.

For macOS, navigate to the root of the repository and do the following (replacing the puzzle number as required):

- `dotnet build`
- `dotnet run --project AoC.Visualisations 2022.12.2`

Run the visualisation project with the visualisation as a parameter.
The format is `year.day.part`, with part being 1 or 2.

There are visualisations for:

- 2018.13.1
- 2018.13.2
- 2018.17.1
- 2020.20.1
- 2022.12.1
- 2022.12.2
- 2022.14.1
- 2022.14.2