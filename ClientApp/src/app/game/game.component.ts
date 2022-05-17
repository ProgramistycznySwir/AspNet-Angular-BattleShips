import { Component } from '@angular/core';
import { Form, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Game } from 'src/models/game';
import { Player } from 'src/models/player';
import { TileData } from 'src/models/tileData';
import { UUID_Validator } from 'src/models/uuidValidator';
import { GameService } from 'src/services/game.service';
import { PlayerService } from 'src/services/player.service';
import {MatDialog} from '@angular/material/dialog';

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
})
export class GameComponent {

  player!: Player
  playerSubID!: number
  game!: Game
  gameID!: string

  gameBoard!: TileData[][]

  get isWaitingForMoveResult(): boolean { return this._gameService.isWaitingForMoveResult }

  public checkIfPlayerSubID(id: number) {
    return this.playerSubID == id
  }

  get labels(): number[] { return Array(10).fill(0).map((_, i) => i); }

  constructor(private _activatedRoute: ActivatedRoute, private _gameService: GameService, private _playerService: PlayerService, public _dialog: MatDialog) {
    _playerService.player.subscribe(next => this.player = next)
    this._activatedRoute.paramMap.subscribe(params => {
        this.gameID = params.get('id')! ?? null;
        this.fetchGame()
      });
    
    _gameService.tiles.subscribe(next => this.recalculateBoard(next))
  }

  private isAlreadyFetchingPlayer: boolean = false
  private fetchGame() {
    if(this.player) {
      this._gameService.getGame(this.gameID, this.player.id)
          .subscribe(next => {
            console.log(next)
            this.game = next;
            if(this.game) {
              this.playerSubID = this.game.players.find(e => e.player_ID == this.player.publicID)!.subID
            }
          })
    }
    else if(this.isAlreadyFetchingPlayer == false) { // Wait with fetch to obtaining player from remote.
      this.isAlreadyFetchingPlayer = true;
      this._playerService.player.subscribe(_ => this.fetchGame())
    }
  }

  private convertTileDataTo2DArray(tileData: TileData[]): TileData[][] {
    var result: TileData[][] = []
    for(var i: number = 0; i < 10; i++) {
      result[i] = [];
      for(var j: number = 0; j< 10; j++) {
        result[i][j] = null!;
      }
    }
    for (let tile of tileData) {
      result[tile.y][tile.x] = tile
    }
    return result
  }
  private recalculateBoard(tileData: TileData[]): void {
    if(tileData)
      this.gameBoard = this.convertTileDataTo2DArray(tileData)
  }

  public onMakeMove(x: number, y: number) {
    if(this.isWaitingForMoveResult == false) {
      this._gameService.makeMove(this.game.id, this.player.id, x, y)
    }
  }

  openDialog() {
    const dialogRef = this._dialog.open(DialogContent_ExplainToUser);

    dialogRef.afterClosed().subscribe(result => {
      console.log(`Dialog result: ${result}`);
    });
  }
}


@Component({
  selector: 'dialog-content-explain-to-user',
  template: `You're not allowed to make this action. App is still in development! It's probably not your turn, or
      you're trying to make invalid move. Eitherway, restarting site may help!`,
})
export class DialogContent_ExplainToUser {}
