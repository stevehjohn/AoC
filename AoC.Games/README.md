# AoC.Games

## 2023.16

This is a simple puzzle game based on "The Floor Will be Lava".

Screen shot <a href="#screenshot">below</a>.

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

`Starts` and `Ends` take the same format. An `X` and `Y` coordination of the position, and a `Direction`. For a `Start`, this is the direction the laser will emit from.
For an `End`, this is the direction the laser must hit the from. You can specify more than one of either.

```json
"Starts": [ { "X": 2, "Y": 2, "Direction": "East" } ]
```

`Blocked` specifies cells where the player cannot place a mirror, and the laser cannot pass through. This is just an array of coordinates.

```json
"Blocked": [ { "X": 27, "Y": 2 } ]
```

<a id="screenshot"></a>
## Screen Shot

![ScreenShot](ScreenShots/Deflectors.png)
