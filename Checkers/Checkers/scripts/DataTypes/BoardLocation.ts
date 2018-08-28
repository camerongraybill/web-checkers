import $ = require("jquery");
import {Piece} from "./Piece";

export class BoardLocation {

    set selected(value: boolean) {
        this._selected = value;
        this.redraw();
    }

    set dom_location(value: HTMLDivElement) {
        this._dom_location = $(value);
        this._dom_location.on("click", () => {
            if (this._callback) {
                this._callback(this);
            }
        });
    }

    set highlighted(value: boolean) {
        this._highlighted = value;
        this.redraw()
    }

    get value(): Piece | null {
        return this._value;
    }

    set value(value: Piece | null) {
        this._value = value;
        this.redraw()
    }

    private redraw() {
        if (this._highlighted) {
            this._dom_location.addClass("availableSquare");
        } else {
            this._dom_location.removeClass("availableSquare");
        }
        if (this._value == null) {
            this._dom_location.html("");
        } else {
            const dom_value = this._value.toDomElement();
            if (this._selected) {
                dom_value.addClass("activePiece");
            }
            this._dom_location.html("");
            this._dom_location.append(dom_value);
        }

    }

    public readonly location: [number, number];

    private _value: Piece | null = null;
    private _highlighted: boolean = false;
    private _selected: boolean = false;
    private _dom_location: JQuery<HTMLDivElement> = $();
    private _callback: Function = () => {};

    constructor(location: [number, number]) {
        this.location = location;
    }

    public registerOnClick(callback: Function): void {
        this._callback = callback;
    }

}
