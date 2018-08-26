export class EndGame {
    public readonly winner: number;
    public readonly reason: number;

    constructor(winner: number, reason: number) {
        this.winner = winner;
        this.reason = reason;
    }

    public static decode(raw: String): EndGame {
        // TODO: This
        return null
    }

}