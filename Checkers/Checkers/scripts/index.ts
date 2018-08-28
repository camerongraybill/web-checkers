import $ = require("jquery");
import {Board} from "./DataTypes/Board";
import {GameConnection} from "./GameConnection";

$(() => {
    const myBoard = new Board();
    for (let row: number = 0; row < 8; ++row) {
        for (let column: number = 0; column < 8; ++column) {
            myBoard.state[row][column].dom_location =
                document.querySelector(("#Location" + row + "x" + column) as string) as HTMLDivElement;
        }
    }

    function StartGame() {

        const connection = new GameConnection(myBoard);
        connection.start();
    }

    $("#landingPage>a").on("click", StartGame);

});
