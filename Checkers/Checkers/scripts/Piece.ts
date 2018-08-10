import {Color} from "./Color";

export class Piece {
    public readonly color: Color;
    public promoted: boolean;

    constructor(color: Color, is_promoted: boolean) {
        this.color = color;
        this.promoted = is_promoted;
    }
}