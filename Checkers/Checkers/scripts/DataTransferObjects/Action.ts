export class Action {
    public readonly from: [number, number];
    public readonly to: [number, number];

    constructor(from: [number, number], to: [number, number]) {
        this.from = from;
        this.to = to;
    }

    public encode(): string {
        console.log(this);
        return JSON.stringify({from: this.from, to: this.to});
    }
}
