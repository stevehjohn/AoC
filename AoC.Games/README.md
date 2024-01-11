# AoC.Games

## 2023.16

This is a simple puzzle game based on "The Floor Will be Lava".

Screen shot <a href="#screenshot">below</a>.

## Running the Game

You can either clone the repository and build it yourself (using Rider, Visual Studio or the command line tools),
or download a pre-built version from the [releases page](https://github.com/stevehjohn/AoC/releases).

You'll need the [dotnet 8 runtime](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) for your platform (Windows/macOS, X64/ARM64).

On macOS, you will probably need to install these dependencies. Make sure you have [homebrew](https://brew.sh/) installed. Then:

- `brew install freeimage`
- `mkdir /usr/local/lib` (this folder may already exist, in which case this step is not needed).
- `sudo ln -s /opt/homebrew/Cellar/freeimage/3.18.0/lib/libfreeimage.dylib /usr/local/lib/libfreeimage`
- `brew install freetype`
- `sudo ln -s /opt/homebrew/lib/libfreetype.6.dylib /usr/local/lib/libfreetype6`

## Configuration

If you find the window is not a good size for you, you can change the size by altering `ScaleFactor` in [app-settings.json](app-settings.json).
This need not bee an integer, if you want the window to be smaller, you can specify `0.5` or for larger `2`.

## Level Definitions

Levels are defined in this [JSON file](Games/Deflectors/Levels/levels.json).

It is an array of levels. `Id` must start at `1` and increment by `1`.

```json
[
  {
    "Id": 1,
    "Starts": [ ],
    "Ends": [ ],
    "Blocked": [ ],
    "Pieces": [ ],
    "Mirrors": [ ]
  },
  {
    "Id": 2,
    etc...
  }
]
```

The playing arena is `30 x 30`, so `X` and `Y` coordinates are in the range `0 .. 29`.

`Starts` and `Ends` take the same format. An `X` and `Y` coordination of the position, and a `Direction`. For a `Start`, this is the direction the laser will emit from.
For an `End`, this is the direction the laser must hit the from. You can specify more than one of either.

```json
"Starts": [ { "X": 2, "Y": 2, "Direction": "East" } ]
```

`Blocked` specifies cells where the player cannot place a mirror, and the laser cannot pass through. This is just an array of coordinates.

```json
"Blocked": [ { "X": 27, "Y": 2 } ]
```

`Mirrors` is an array of mirrors already on the board when the level starts. It takes `X`, `Y` and `Piece` properties.
Where Piece is `|`, `-`, `/` or `\\` (double backslash because of JSON escaping convention).

```json
"Mirrors": [ { "X": 15, "Y": 15, "Piece": "/" }, { "X": 20, "Y": 15, "Piece": "\\" } ]
```

Lastly, `Pieces` is and array of mirrors in the order the player will be presented with them. The same values are allowed as with `Mirrors`.

```json
"Pieces": [ "/", "-", "|", "\\", "/" ]
```

The level is complete when a beam is hitting all the `Ends`.

<a id="screenshot"></a>
## Screen Shot

![ScreenShot](ScreenShots/Deflectors.png)

