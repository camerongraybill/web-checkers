import {Piece} from "./Piece";

export class BoardLocation {

    private _value: Piece | null = null;
    private _highlighted: boolean = false;
    private _dom_location: HTMLDivElement = null;
    public readonly location: [number, number];

    constructor(location: [number, number]) {
        this.location = location;
    }

    set dom_location(value:HTMLDivElement) {
        this._dom_location = value;
    }

    set value(value: Piece | null) {
        // Update Div here
        this._value = value;
    }

    set highlighted(value: boolean) {
        // Update Div here
        this._highlighted = value;
    }

    public registerOnClick(callback:Function): void {

    }

}