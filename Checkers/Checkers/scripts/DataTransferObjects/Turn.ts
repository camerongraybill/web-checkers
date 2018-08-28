export class Turn {

    public static decode(raw: string): Turn {
        const turn_json = JSON.parse(raw);
        return new Turn(turn_json.board, turn_json.moves);
    }
    public readonly raw_board: any;
    public readonly raw_moves: any[];

    constructor(raw_board: any, raw_moves: any[]) {
        this.raw_board = raw_board;
        this.raw_moves = raw_moves;
    }
}
