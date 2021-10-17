# Battleships
Simple simulation of Battleships game ([Wiki](https://en.wikipedia.org/wiki/Battleship_(game)))

## Assumptions
1. Default game settings:
Board size: 12x12

| Class of ship   | Size | No. of ships |
| --------------- | ---- | ------------ |
| Carrier         |  5   |      1       |
| Battleship      |  4   |      2       |
| Cruiser         |  3   |      3       |
| Submarine       |  3   |      3       |
| Destroyer       |  2   |      4       |

1. User may modify the default settings as long as it is possible to generate valid board provided parameters.

1. Placement of the ships is generated randomly, but can fail (it's non-deterministic).
In such case the user can retry generation with different Game parameters.

1. In generation, ships can touch each other, but they can't occupy the same field.

1. Game finishes when at least one of the players shoots all enemy ships. 
If both players do it at the same time, then the game announces draw and there's no winner.
Otherwise, the winner is the player who does it first. 

