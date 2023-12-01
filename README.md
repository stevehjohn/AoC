# AoC

Stevo's Advent of Code Repo.

See answers and low-res timings [here](results.md).

# Visualisations

Now supports cross-platform visualisations. YouTube playlist of them [here](https://www.youtube.com/playlist?list=PLBtwzTaAY-IWq6Mi1nvwsphMTw-HU13eM).

Navigate to the root of the repository and do the following (replacing the puzzle number as required):

- `dotnet build`
- `dotnet run --project AoC.Visualisations 2022.12.2`

If on Windows you can automatically save a recording of the visualisation (say, for uploading to YouTube). Specify the path as a second parameter.

- `dotnet run --project AoC.Visualisations 2022.12.2 C:\Videos\AoC\visualisation.avi`

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
