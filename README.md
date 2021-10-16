# Battleships
Simple simulation of Battleships game ([Wiki](https://en.wikipedia.org/wiki/Battleship_(game)))

## Assumptions
1. Default settings:
Board size: 10x10

| Class of ship   | Size | No. of ships |
| --------------- | ---- | ------------ |
| Carrier         |  5   |      1       |
| Battleship      |  4   |      2       |
| Cruiser         |  3   |      3       |
| Submarine       |  3   |      3       |
| Destroyer       |  2   |      4       |

1. User may modify the default settings as long as it is possible to generate valid board provided parameters.

1. Ships can touch each other, but they can't occupy the same grid space

1. Placement of the ships is generated randomly, but can fail (it's non-deterministic).
In such case the User can retry generation with different Game parameters.
 




