import $ = require("jquery");
import {Piece} from "./Piece";

export class BoardLocation {

    set selected(value: boolean) {
        this.innerSelected = value;
        this.redraw();
    }

    set dom_location(value: HTMLDivElement) {
        this.innerDomLocation = $(value);
        this.innerDomLocation.on("click", () => {
            if (this.callback) {
                this.callback(this);
            }
        });
    }

    set highlighted(value: boolean) {
        this.innerHighlight = value;
        this.redraw();
    }

    get value(): Piece | null {
        return this.innerValue;
    }

    set value(value: Piece | null) {
        this.innerValue = value;
        this.redraw();
    }

    public readonly location: [number, number];

    private innerValue: Piece | null = null;
    private innerHighlight: boolean = false;
    private innerSelected: boolean = false;
    private innerDomLocation: JQuery<HTMLDivElement> = $();

    constructor(location: [number, number]) {
        this.location = location;
    }

    public registerOnClick(callback: (me: BoardLocation) => void): void {
        this.callback = callback;
    }

    private redraw() {
        if (this.innerHighlight) {
            this.innerDomLocation.addClass("availableSquare");
        } else {
            this.innerDomLocation.removeClass("availableSquare");
        }
        if (this.innerValue == null) {
            this.innerDomLocation.html("");
        } else {
            const domValue = this.innerValue.toDomElement();
            if (this.innerSelected) {
                domValue.addClass("activePiece");
            }
            this.innerDomLocation.html("");
            this.innerDomLocation.append(domValue);
        }

    }

    private callback: (me: BoardLocation) => void = () => null;

}
