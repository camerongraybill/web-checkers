import {Piece} from "./Piece";
import $ = require("jquery");

export class BoardLocation {

    private _value: Piece | null = null;
    private _highlighted: boolean = false;
    private _dom_location: JQuery<HTMLDivElement> = $();
    public readonly location: [number, number];

    constructor(location: [number, number]) {
        this.location = location;
    }

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

    public registerOnClick(callback: Function): void {
        this._dom_location.on("click", () => {
            callback(this);
        });
    }

}