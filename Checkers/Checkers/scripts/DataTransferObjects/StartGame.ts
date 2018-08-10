export class StartGame {
    public readonly color: number;
    public readonly raw_board: String;

    constructor(color: number, raw_board: String) {
        this.color = color;
        this.raw_board = raw_board;
    }

    public static decode(raw: String): StartGame {
        return null
    }
}