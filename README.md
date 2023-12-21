# AoC

Stevo's Advent of Code Repo.

See answers and low-res timings [here](results.md).

## Solutions

To run the solutions, navigate to the root of the repository in a terminal, command prompt or shell. Then execute the relevant command.

- `./run.sh` for macOS (and Linux in theory, but untested).
- `run.bat` for Windows.

Both can take the puzzle year (4 digits), day (2 digits) and part (1 digit) as a parameter. Examples:

- `./run.sh 2023`
- `./run.sh 2023.01`
- `./run.sh 2023.01.2`

## Visualisations

Now supports cross-platform visualisations. YouTube playlist of them [here](https://www.youtube.com/playlist?list=PLBtwzTaAY-IWq6Mi1nvwsphMTw-HU13eM).

Navigate to the root of the repository and do the following (replacing the puzzle number as required):

- `dotnet run -c Release --project AoC.Visualisations 2022.12.2`

If on Windows you can automatically save a recording of the visualisation (say, for uploading to YouTube). Specify the path as a second parameter.

- `dotnet run -c Release --project AoC.Visualisations 2022.12.2 C:\Videos\AoC\visualisation.avi`

Run the visualisation project with the visualisation as a parameter.
The format is `year.day.part`, with part being 1 or 2.

There are visualisations for:

- 2018.13.1
- 2018.13.2
- 2018.17.1
- 2019.18.2
- 2020.20.1
- 2022.12.1
- 2022.12.2
- 2022.14.1
- 2022.14.2
- 2023.10.2
- 2023.14.2
- 2023.16.1
- 2023.16.2
- 2023.17.1
- 2023.17.2
