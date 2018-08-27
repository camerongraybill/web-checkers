import {BoardLocation} from "./BoardLocation";
export class Board {

    set on_move_callback(value: Function) {
        this._on_move_callback = value;
    }

    public static fromString(raw: string): Board {
        // TODO: This
        return new Board();
    }
    public state: BoardLocation[][];
    private _on_move_callback: Function;
    private last_clicked: BoardLocation|null;

    constructor() {
        this.state = [];
        for (let row: number = 0; row < 8; ++row) {
            this.state[row] = [];
            for (let column: number = 0; column < 8; ++column) {
                this.state[row][column] = new BoardLocation([row, column]);
                this.state[row][column].registerOnClick(this.onBoardClick);
            }
        }
    }

    public updateFromOtherBoard(new_board: Board): void {
        for (let row: number = 0; row < 8; ++row) {
            for (let column: number = 0; column < 8; ++column) {
                this.state[row][column].value = new_board.state[row][column].value;
            }
        }
    }

    private onBoardClick(location: BoardLocation) {
        // do something because of a click, sometimes call self._on_move_callback
        if (this.last_clicked != null && this.last_clicked != location) {
            // Case where the user clicked a different square
            this._on_move_callback(this.last_clicked, location);
            this.last_clicked = null;
        } else if (this.last_clicked != null) {
            // Case where the user clicked the same square
            this.last_clicked.highlighted = false;
            this.last_clicked = null;
        } else {
            // Case where the user clicked a square that was not highlighted
            this.last_clicked = location;
            this.last_clicked.highlighted = true;
        }
    }
}
