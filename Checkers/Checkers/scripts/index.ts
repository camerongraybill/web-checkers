import $ = require("jquery");
import {Board} from "./DataTypes/Board";
import {GameConnection} from "./GameConnection";

$(() => {
    const my_board = new Board();
    for (let row: number = 0; row < 8; ++row) {
        for (let column: number = 0; column < 8; ++column) {
            my_board.state[row][column].dom_location = document.querySelector(("#Location" + row + "x" + column) as string) as HTMLDivElement;
        }
    }

    function StartGame() {

        const connection = new GameConnection(my_board);
        connection.start();
        console.log(my_board);
    }

    $("#landingPage>a").on("click", StartGame);

});
