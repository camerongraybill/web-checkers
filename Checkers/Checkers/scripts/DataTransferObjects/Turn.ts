export class Turn {
    public readonly raw_board: String;
    public readonly raw_moves: String[];

    constructor(raw_board: String, raw_moves: String[]) {
        this.raw_board = raw_board;
        this.raw_moves = raw_moves;
    }


    public static decode(raw: String): Turn {
        return null
    }
}