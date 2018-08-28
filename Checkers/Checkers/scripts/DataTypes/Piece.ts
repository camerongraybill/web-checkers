import $ = require("jquery");
import {Color} from "./Enums";

export class Piece {
    public readonly color: Color;
    public promoted: boolean;

    constructor(color: Color, isPromoted: boolean) {
        this.color = color;
        this.promoted = isPromoted;
    }

    public toDomElement(): JQuery<HTMLElement> {
        const checker = $("<div class='boardChecker'></div>");
        switch (this.color) {
            case Color.BLACK: checker.addClass("blackChecker"); break;
            case Color.RED: checker.addClass("whiteChecker"); break;
        }
        if (this.promoted) {
            switch (this.color) {
                case Color.BLACK: checker.addClass("blackKing"); break;
                case Color.RED: checker.addClass("whiteKing"); break;
            }
        }

        return checker;
    }
}
