import {GameConnection} from "./GameConnection";
import {Board} from "./DataTypes/Board";
import $ = require("jquery");

$(() => {
    const my_board = new Board();
    for (let row: number = 0; row < 8; ++row)
        for (let column: number = 0; column < 8; ++column)
            my_board.state[row][column].dom_location = document.querySelector(("#Location" + row + "x" + column) as string) as HTMLDivElement;



    function StartGame() {
        
        const connection = new GameConnection(my_board);
        connection.start();
    }
    
    $("#landingPage>a").on("click", StartGame);
    
    
    
});
