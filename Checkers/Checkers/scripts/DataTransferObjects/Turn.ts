export class Turn {

    public static decode(raw: string): Turn {
        // TODO: This
        return null;
    }
    public readonly raw_board: string;
    public readonly raw_moves: string[];

    constructor(raw_board: string, raw_moves: string[]) {
        this.raw_board = raw_board;
        this.raw_moves = raw_moves;
    }
}
