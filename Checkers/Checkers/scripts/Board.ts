import {BoardLocation} from "./BoardLocation";
import {GameConnection} from "./GameConnection";

export class Board {
    public state: BoardLocation[][];
    private readonly dom_location: HTMLDivElement;

    constructor(div: HTMLDivElement) {
        this.dom_location = div;
        this.state = [];
        for (let i: number = 0; i < 8; ++i) {
            this.state[i] = [];
            for (let j: number = 0; j < 8; ++j) this.state[i][j] = new BoardLocation([i,j]);
        }
    }

    public registerConnection(conn: GameConnection) {
        // Register callbacks into BoardLocations
    }

    public updateFromString(board_str: String): void {

    }
}