# Battleships
Simple simulation of Battleships game ([Wiki](https://en.wikipedia.org/wiki/Battleship_(game)))

## Description
1. Default game settings:  
Board size: 12x12  
  
| Class of ship   | Size | No. of ships |
| --------------- | ---- | ------------ |
| Carrier         |  5   |      1       |
| Battleship      |  4   |      2       |
| Cruiser         |  3   |      3       |
| Submarine       |  3   |      3       |
| Destroyer       |  2   |      4       |
  
2. User may modify the default settings as long as it is possible to generate valid board provided parameters.

1. There is not much validation against malicious user input, the program assumes good will of the user.

1. Placement of the ships is generated randomly, but can fail (it's non-deterministic).  
In such case the user can retry generation with different game parameters.

1. Each ship occupies a number of consecutive squares on the grid, arranged either horizontally or vertically. The number of squares for each ship is determined by the type of the ship. The ships cannot overlap (i.e., only one ship can occupy any given square in the grid). The types and numbers of ships allowed are the same for each player. 

1. Game process consists of turns. In each turn each player shoots selected field on the enemy board.  
In current state **selection is completely random**.

1. Game finishes when at least one of the players shoots all enemy ships. 
If both players do it at the same time (in same turn), then the game announces draw and there's no winner.  
Otherwise, the winner is the player who does it first. 



