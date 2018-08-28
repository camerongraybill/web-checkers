export class EndGame {

    public static decode(raw: string): EndGame {
        const response_json = JSON.parse(raw);
        return new EndGame(response_json.winner,  response_json.reason);
    }
    public readonly winner: number;
    public readonly reason: number;

    constructor(winner: number, reason: number) {
        this.winner = winner;
        this.reason = reason;
    }

}
