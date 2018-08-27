export class StartGame {

    public static decode(raw: string): StartGame {
        // TODO: This
        return null;
    }
    public readonly color: number;
    public readonly raw_board: string;

    constructor(color: number, raw_board: string) {
        this.color = color;
        this.raw_board = raw_board;
    }
}
