export class EndGame {

    public static decode(raw: string): EndGame {
        const responseJson = JSON.parse(raw);
        return new EndGame(responseJson.winner,  responseJson.reason);
    }
    public readonly winner: number;
    public readonly reason: number;

    constructor(winner: number, reason: number) {
        this.winner = winner;
        this.reason = reason;
    }

}
