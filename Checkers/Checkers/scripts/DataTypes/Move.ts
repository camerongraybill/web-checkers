import {Board} from "./Board";
import {BoardLocation} from "./BoardLocation";

export class Move {

    public static fromString(raw: String, on_board: Board): Move {
        // TODO: This
        return null;
    }
    public readonly source: BoardLocation;
    public readonly destination: BoardLocation;

    constructor(source: BoardLocation, destination: BoardLocation) {
        this.source = source;
        this.destination = destination;
    }
}
