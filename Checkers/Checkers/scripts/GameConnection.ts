import * as signalR from "@aspnet/signalr";
import $ = require("jquery");
import {Action} from "./DataTransferObjects/Action";
import {EndGame} from "./DataTransferObjects/EndGame";
import {StartGame} from "./DataTransferObjects/StartGame";
import {Turn} from "./DataTransferObjects/Turn";
import {Board} from "./DataTypes/Board";
import {BoardLocation} from "./DataTypes/BoardLocation";
import {Color, GameEndReason} from "./DataTypes/Enums";
import {Move} from "./DataTypes/Move";

const GAME_CONNECTION_URL = "/game";

export class GameConnection {
    private readonly board: Board;
    private readonly connection: signalR.HubConnection = null;
    private innerMyColor: Color = null;
    private gameEnded: boolean = false;

    constructor(board: Board) {
        this.board = board;
        this.connection = new signalR.HubConnectionBuilder().withUrl(GAME_CONNECTION_URL).build();
        this.initializeCallbacks();
    }

    private set my_color(value: Color) {
        switch (value) {
            case Color.BLACK:
                $("#oTeam").text("BLUE TEAM");
                break;
            case Color.RED:
                $("#oTeam").text("RED TEAM");
                break;
        }
        this.innerMyColor = value;
    }

    public start(): void {
        this.connection.start().then(() => {
            $("#landingPage").addClass("hiddenPage closedPage");
            $("#publicMatchPage").removeClass("hiddenPage closedPage");
        });
    }

    private initializeCallbacks(): void {
        this.connection
            .on("gameStart", (data: string) => this.on_game_start(StartGame.decode(data)));
        this.connection.on("yourMove", (data: string) => this.on_your_turn(Turn.decode(data)));
        this.connection.on("gameEnd", (data: string) => this.on_game_end(EndGame.decode(data)));
        this.board.on_move_callback = (from: BoardLocation, to: BoardLocation) => this.sendMove(from, to);
    }

    private on_game_end(data: EndGame): void {
        // Do something with the winner and reason
        if (!this.gameEnded) {
            const endGameReason: GameEndReason = data.reason;
            const winner: Color = data.winner;
            if (endGameReason === GameEndReason.OPPONENT_DISCONNECT) {
                $("#oEnd").text("Your opponent disconnected");
            } else if (winner === this.innerMyColor) {
                $("#oEnd").text("You won!");
            } else {
                $("#oEnd").text("You lost");
            }

            $("#gamePage").addClass("hiddenPage closedPage");
            $("#endPage").removeClass("hiddenPage closedPage");
            this.gameEnded = true;
        }
    }

    private on_game_start(data: StartGame): void {
        if (this.innerMyColor === null) {
            if (data.color === Color.BLACK) {
                $("#board").addClass("rotateBoard");
            }
            $("#totalContainer").addClass("hiddenPage closedPage");
            $("#gamePage").removeClass("hiddenPage closedPage");
            this.board.updateFromOtherBoard(Board.fromJSON(data.rawBoard));
            this.my_color = data.color;
        }
    }

    private on_your_turn(data: Turn): void {
        this.board.updateFromOtherBoard(Board.fromJSON(data.rawBoard));
        this.board.legal_moves = data.rawMoves
            .map((rawMoveObject: any) => Move.fromJSON(rawMoveObject, this.board));
        $("#oTeam").addClass("activePlayer");
    }

    private sendMove(from: BoardLocation, to: BoardLocation): void {
        this.connection.send("onMove", (new Action(from.location, to.location)).encode())
            .then(() => $("#oTeam").removeClass("activePlayer"));
    }
}
