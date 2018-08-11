import {GameConnection} from "./GameConnection";
import {Board} from "./Board";

let my_board = new Board(document.querySelector("#BoardDiv") as HTMLDivElement);

for (let i: number = 0; i < 8; ++i)
    for (let j: number = 0; j < 8; ++j)
        my_board.state[i][j].dom_location = document.querySelector(("#Location" + i + "x" + j) as String) as HTMLDivElement;

let connection = new GameConnection(my_board);