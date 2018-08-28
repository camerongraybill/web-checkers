import *  as signalR from "@aspnet/signalr";
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
    private _my_color: Color = null;

    constructor(board: Board) {
        this.board = board;
        this.connection = new signalR.HubConnectionBuilder().withUrl(GAME_CONNECTION_URL).build();
        this.initializeCallbacks();
    }

    private get my_color(): Color {
        return this._my_color;
    }

    private set my_color(value: Color) {
        const team_screen = $("#oTeam");
        switch (value) {
            case Color.BLACK:
                team_screen.text("BLACK TEAM");
                break;
            case Color.RED:
                team_screen.text("RED TEAM");
                break;
        }
        this._my_color = value;
    }

    public start(): void {
        this.connection.start().catch((err) => console.error(err)).then(() => {
            $("#landingPage").addClass("hiddenPage closedPage");
            $("#publicMatchPage").removeClass("hiddenPage closedPage");
        });
    }

    private initializeCallbacks(): void {
        this.connection.on("gameStart", (data: string) => this.on_game_start(StartGame.decode(data)));
        this.connection.on("yourMove", (data: string) => this.on_your_turn(Turn.decode(data)));
        this.connection.on("gameEnd", (data: string) => this.on_game_end(EndGame.decode(data)));
        this.board.on_move_callback = (from: BoardLocation, to: BoardLocation) => this.sendMove(from, to);
    }

    private on_game_end(data: EndGame): void {
        // Do something with the winner and reason
        const end_reason: GameEndReason = data.reason;
        const winner: Color = data.winner;
        const winner_color = (() => {
            switch (winner) {
                case Color.RED: return "red";
                case Color.BLACK: return "black";
            }
        })();
        if (end_reason === GameEndReason.OPPONENT_DISCONNECT) {
            console.log("Your opponent Disconnected");
        }
        $("#oEnd").text(winner_color + " won!");
        $("#gamePage").addClass("hiddenPage closedPage");
        $("#endPage").removeClass("hiddenPage closedPage");
    }

    private on_game_start(data: StartGame): void {
        if (this.my_color !== null) {
            console.error("Got game start message when game was already started");
        } else {
            $("#totalContainer").addClass("hiddenPage closedPage");
            $("#gamePage").removeClass("hiddenPage closedPage");
            const new_board: Board = Board.fromJSON(data.raw_board);
            this.board.updateFromOtherBoard(new_board);
            this.my_color = data.color;
        }
    }

    private on_your_turn(data: Turn): void {
        this.board.updateFromOtherBoard(Board.fromJSON(data.raw_board));
        const possible_moves = data.raw_moves
            .map((raw_move_object: any) => Move.fromJSON(raw_move_object, this.board));
        this.board.legal_moves = possible_moves;
        $("#oTeam").addClass("activePlayer");
    }

    private sendMove(from: BoardLocation, to: BoardLocation): void {
        this.connection.send("onMove", (new Action(from.location, to.location)).encode())
            .then(() => $("#oTeam").removeClass("activePlayer"));
    }
}