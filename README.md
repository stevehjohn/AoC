# AoC

Stevo's Advent of Code Repo.

See timings [here](results.md).

## Games

Only two so far, but maybe more to come. See the [AoC.Games README](AoC.Games/README.md).

To run the games, navigate to the root of the repository in a terminal, then for macOS:

- `./run.sh mirrors` or
- `./run.sh mazes`

For Windows:

- `run.bat mirrors` or
- `run.bat mazes`

## Solutions

To run the solutions, navigate to the root of the repository in a terminal, command prompt or shell. Then execute the relevant command.

- `./run.sh` for macOS (and Linux in theory, but untested).
- `run.bat` for Windows.

Both can take the puzzle year (4 digits), day (2 digits) and part (1 digit) as a parameter. Examples:

- `./run.sh` runs all puzzles.
- `./run.sh 2023` runs puzzles from 2023.
- `./run.sh 2023.01` runs both parts of the puzzle from 2023 day 1.
- `./run.sh 2023.01.2` runs part 2 of the puzzle for 2023 day 1.

## Visualisations

Now supports cross-platform visualisations. YouTube playlist of them [here](https://www.youtube.com/playlist?list=PLBtwzTaAY-IWq6Mi1nvwsphMTw-HU13eM).

To run the visualisation, navigate to the root of the repository in a terminal, command prompt or shell. 
Then execute the relevant command with the desired visualisation as a parameter.

- `./visualise.sh 2023.16.1` for macOS (and Linux in theory, but untested).
- `visualise.bat 2023.16.1` for Windows.

The parameter is exactly the same as for `run.[bat|sh]` as above; `yyyy.dd.p` with `p` being `1` or `2`.

If using Windows you can automatically save a recording of the visualisation (say, for uploading to YouTube). Specify the path as a second parameter.

- `visualise.bat 2023.16.1 C:\Videos\AoC\visualisation.avi`

There are visualisations for:

- 2018.13.1 - Mine Cart Madness
- 2018.13.2
- 2018.17.1 - Reservoir Research
- 2019.18.2 - Many Worlds Interpretation
- 2020.20.1 - Jurassic Jigsaw
- 2022.12.1 - Hill Climbing Algorithm
- 2022.12.2
- 2022.14.1 - Regolith Reservoir
- 2022.14.2
- 2022.24.1 - Blizzard Basin
- 2023.10.2 - Pipe Maze
- 2023.14.2 - Parabolic Reflector Dish
- 2023.16.1 - The Floor Will be Lava
- 2023.16.2
- 2023.17.1 - Clumsy Crucible
- 2023.17.2
- 2023.22.1 - Sand Slabs
- 2023.22.2
- 2024.09.1 - Disk Fragmenter
- 2024.09.2
- 2024.10.1 - Hoof It
- 2024.10.2
- 2024.15.1 - Warehouse Woes
- 2024.15.2
- 2024.18.2 - RAM Run
- 2024.20.1 - Race condition

## Mental Notes to Self

The `.encrypted` files, safe to push to GitHub, have to be deleted to be recreated from the `.clear` versions.