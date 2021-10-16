# Battleships
Simple simulation of Battleships game ([Wiki](https://en.wikipedia.org/wiki/Battleship_(game)))

## Assumptions
1. Default settings:

| Class of ship   | Size | No. of ships |
| --------------- | ---- | ------------ |
| Carrier	      |  5   |      1       |
| Battleship      |  4   |      2       |
| Cruiser	      |  3   |      3       |
| Submarine	      |  3   |      3       |
| Destroyer	      |  2   |      4       |

2. Ships can touch each other, but they can't occupy the same grid space

3. Placement of the ships is generated randomly, but can fail (it's non-deterministic).
In such case User can retry generation with different Game parameters.
 




