import {BoardLocation} from "./BoardLocation";
import {Color} from "./Enums";
import {Move} from "./Move";
import {Piece} from "./Piece";

export class Board {
    get legal_moves(): Move[] {
        return this.legalMoves;
    }

    set legal_moves(value: Move[]) {
        this.legalMoves = value;
        this.clearHighlights();
        this.highlightLegalStarts();
    }

    set on_move_callback(value: (from: BoardLocation, to: BoardLocation) => void) {
        this.onMoveCallback = value;
    }

    public static fromJSON(board: any): Board {
        const newBoard = new Board();
        board.Pieces.filter((boardLocation: object | null) => boardLocation).forEach((boardLocation: any) => {
            newBoard.state
                [boardLocation.Location.Item2]
                [boardLocation.Location.Item1].value = new Piece(boardLocation.Player, boardLocation.IsKing);
        });
        return newBoard;
    }

    public state: BoardLocation[][];
    private legalMoves: Move[] = [];
    private onMoveCallback: (from: BoardLocation, to: BoardLocation) => void;
    private selectedPiece: BoardLocation | null = null;

    constructor() {
        this.state = [];
        for (let x: number = 0; x < 8; ++x) {
            this.state[x] = [];
        }
        for (let row: number = 0; row < 8; ++row) {
            for (let column: number = 0; column < 8; ++column) {
                this.state[column][row] = new BoardLocation([row, column]);
                this.state[column][row]
                    .registerOnClick((clickedLocation: BoardLocation) => this.onBoardClick(clickedLocation));
            }
        }
    }

    public updateFromOtherBoard(otherBoard: Board): void {
        for (let row: number = 0; row < 8; ++row) {
            for (let column: number = 0; column < 8; ++column) {
                this.state[column][row].value = otherBoard.state[column][row].value;
            }
        }
    }

    private clearHighlights() {
        for (let row: number = 0; row < 8; ++row) {
            for (let column: number = 0; column < 8; ++column) {
                this.state[row][column].highlighted = false;
            }
        }
    }

    private highlightLegalStarts() {
        this.legalMoves.map((item: Move) => item.source).forEach((item: BoardLocation) => item.highlighted = true);
    }

    private onBoardClick(clickedLocation: BoardLocation) {
        // do something because of a click, sometimes call self._on_move_callback
        if (this.selectedPiece !== null) {
            // There is already a piece selected
            if (this.selectedPiece === clickedLocation) {
                // Picked the same piece to deselect it
                this.selectedPiece.selected = false;
                this.selectedPiece = null;
                this.clearHighlights();
                this.highlightLegalStarts();
            } else {
                // Different piece is selected
                if (this.legalMoves
                    .filter((item: Move) => item.source === this.selectedPiece)
                    .map((item: Move) => item.destination).indexOf(clickedLocation) !== -1) {
                    // This is a legal move to move to
                    this.onMoveCallback(this.selectedPiece, clickedLocation);
                    if (Math.abs(this.selectedPiece.location[0] - clickedLocation.location[0]) === 2) {
                        // It was a jump so remove the jumped piece
                        this.state[(this.selectedPiece.location[1] + clickedLocation.location[1]) / 2]
                            [(this.selectedPiece.location[0] + clickedLocation.location[0]) / 2].value = null;
                    }
                    // Promote the piece if need be
                    if (clickedLocation.location[1] === 0 && this.selectedPiece.value.color === Color.RED) {
                        this.selectedPiece.value.promoted = true;
                    } else if (clickedLocation.location[1] === 7 && this.selectedPiece.value.color === Color.BLACK) {
                        this.selectedPiece.value.promoted = true;
                    }

                    // Move the piece
                    clickedLocation.value = this.selectedPiece.value;
                    // Null out the moved from location
                    this.selectedPiece.value = null;
                    this.selectedPiece = null;
                    this.clearHighlights();
                    // Clear out leagal moves as well
                    this.legalMoves = [];
                }
            }
        } else {
            // There is no piece selected
            if (this.legalMoves.map((item: Move) => item.source).indexOf(clickedLocation) !== -1) {
                // If it is a valid starting piece
                this.clearHighlights();
                this.selectedPiece = clickedLocation;
                // Highlight possible destinations
                this.legalMoves
                    .filter((item: Move) => item.source === this.selectedPiece)
                    .map((item: Move) => item.destination)
                    .forEach((item: BoardLocation) => item.highlighted = true);
            }
        }
    }
}
