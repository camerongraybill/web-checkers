import {BoardLocation} from "./BoardLocation";
import {Board} from "./Board";

export class Move {
    public readonly source: BoardLocation;
    public readonly destination: BoardLocation;

    constructor(source: BoardLocation, destination: BoardLocation) {
        this.source = source;
        this.destination = destination;
    }

    public static fromString(raw: String, on_board: Board): Move {
        // TODO: This
        return null;
    }
}