import {Board} from "./Board";
import {BoardLocation} from "./BoardLocation";

export class Move {

    public static fromJSON(raw: any, on_board: Board): Move {
        console.log(raw,  on_board);
        return new Move(on_board.state[raw.Piece.Location.Item2][raw.Piece.Location.Item1], on_board.state[raw.MoveTo.Item2][raw.MoveTo.Item1]);
    }
    public readonly source: BoardLocation;
    public readonly destination: BoardLocation;

    constructor(source: BoardLocation, destination: BoardLocation) {
        this.source = source;
        this.destination = destination;
    }
}
