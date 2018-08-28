export class StartGame {

    public static decode(raw: string): StartGame {
        let response_json: any = JSON.parse(raw);
        
        return new StartGame(response_json.player, response_json.board);
    }
    public readonly color: number;
    public readonly raw_board: any;

    constructor(color: number, raw_board: any) {
        this.color = color;
        this.raw_board = raw_board;
    }
}
