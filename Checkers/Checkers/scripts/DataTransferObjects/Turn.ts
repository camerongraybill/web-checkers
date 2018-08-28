export class Turn {

    public static decode(raw: string): Turn {
        const turnJson = JSON.parse(raw);
        return new Turn(turnJson.board, turnJson.moves);
    }
    public readonly rawBoard: any;
    public readonly rawMoves: any[];

    constructor(rawBoard: any, rawMoves: any[]) {
        this.rawBoard = rawBoard;
        this.rawMoves = rawMoves;
    }
}
