export class StartGame {

    public static decode(raw: string): StartGame {
        const responseJson: any = JSON.parse(raw);

        return new StartGame(responseJson.player, responseJson.board);
    }
    public readonly color: number;
    public readonly rawBoard: any;

    constructor(color: number, rawBoard: any) {
        this.color = color;
        this.rawBoard = rawBoard;
    }
}
