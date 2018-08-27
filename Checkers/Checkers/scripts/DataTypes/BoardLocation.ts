import $ = require("jquery");
import {Piece} from "./Piece";

export class BoardLocation {

    set dom_location(value: HTMLDivElement) {
        this._dom_location = $(value);
    }

    set highlighted(value: boolean) {
        // TODO: This
        this._highlighted = value;
    }

    set value(value: Piece | null) {
        // Update Div here
        if (value == null) {
            this._dom_location.html("");
        } else {
            this._dom_location.html(value.asHTML);
        }
        this._value = value;
    }
    public readonly location: [number, number];

    private _value: Piece | null = null;
    private _highlighted: boolean = false;
    private _dom_location: JQuery<HTMLDivElement> = $();

    constructor(location: [number, number]) {
        this.location = location;
    }

    public registerOnClick(callback: Function): void {
        this._dom_location.on("click", () => {
            callback(this);
        });
    }

}
