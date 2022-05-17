import { HttpClient, HttpStatusCode } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AppSettings } from 'src/AppSettings';
import { BehaviorSubject, interval, Observable, Subject } from 'rxjs';
import { WebSocketService } from './websocket.service';
import { environment } from 'src/environments/environment';
import { Game } from 'src/models/game';
import { TileData } from 'src/models/tileData';
import { HttpResponse } from '@microsoft/signalr';
import { TileDataUpdate } from 'src/models/tileDataUpdate';
import { PlayerService } from './player.service';

@Injectable({
  providedIn: 'root'
})
export class GameService {

  // games: BehaviorSubject<Game[]> = new BehaviorSubject<Game[]>(null!)
  game: BehaviorSubject<Game> = new BehaviorSubject<Game>(null!)
  tiles: BehaviorSubject<TileData[]> = new BehaviorSubject<TileData[]>(null!)

  constructor(private _httpClient: HttpClient, private _playerService: PlayerService, private _webSocketService: WebSocketService) {
    this.game.subscribe(next => this.tiles.next(next?.boardData))
    // _webSocketService.subscribeToTileData(this.addTile)
    //TODO: Replace this repeated fetching with WebSockets.
    interval(5000).subscribe(next => this.checkForBoardUpdates())
  }



  public getGame(gameID: string, perspectiveID: string): Observable<Game> {
    //TODO: Reinstantiate error checks.
    // if(!AppSettings.UUID_REGEX.test(gameID))
    //   throw Error("Invalid UUID")
    // if(!AppSettings.UUID_REGEX.test(perspectiveID))
    //   throw Error("Invalid UUID")
    
    this._httpClient.get(`${environment.API_ENDPOINT}Game/${gameID}/${perspectiveID}`)
        .pipe(res => { console.info(res); return res})
        .subscribe(
            res => this.game.next(res as Game),
            err => console.error(err)
          )
    return this.game;
  }
  public createGame(publicID1: string, publicID2?: string) {
    // TODO: Fix checks (don't know why but they trigger at weird moments...).
    // if(!AppSettings.UUID_REGEX.test(publicID1))
    //   throw Error(`Invalid publicID1: ${publicID1}`)
    // if(publicID2 && !AppSettings.UUID_REGEX.test(publicID2))
    //   throw Error(`Invalid publicID2: ${publicID2}`)
    return this._httpClient.post(`${environment.API_ENDPOINT}Game/CreateGame`,
        {
          player1_ID: publicID1,
          player2_ID: publicID2
        })
        .pipe(res => { console.info(res); return res})
        .subscribe(
            res => this.game.next(res as Game),
            err => console.error(err)
          )
  }
  
  private _isWaitingForMoveResult: boolean = false
  get isWaitingForMoveResult(): boolean { return this._isWaitingForMoveResult }
  public makeMove(gameID: string, privateID: string, x: number, y: number) {
    // TODO: Implement error checks.
    this._isWaitingForMoveResult = true;
    let request = this._httpClient.post<TileData>(`${environment.API_ENDPOINT}Game/AddMove`, {
        gameID: gameID,
        playerID: privateID,
        x: x,
        y: y
      })
          .pipe(res => { console.info(res); return res})
          .subscribe(res => { this.addTile(res); this._isWaitingForMoveResult= false})
    // return request
  }
  private addTile(tile: TileData) {
    let game = Object.create(this.game.getValue()) as Game
    game.turn = (game.turn + (tile.isHit ? 0 : 1)) % game.players.length
    game.boardData = [...this.tiles.getValue(), tile]
    // game.lastMove = new Date()
    this.game.next(game)
  }
  private addTiles(update: TileDataUpdate) {
    let game = Object.create(this.game.getValue()) as Game
    game.turn = update.turn
    game.boardData = update.tiles
    game.lastMove = new Date(update.lastMove)
    this.game.next(game)
  }

  private checkForBoardUpdates(): void {
    if(this.game == null || this.game.getValue() == null)
      return;
    let game = this.game.getValue()
    let player = this._playerService.player.getValue()
    let lastUpdate = new Date(game.lastMove).toISOString()
    let requestUrl = `${environment.API_ENDPOINT}Game/CheckUpdate/${game.id}/${player.id}/${lastUpdate}`
    // console.log(requestUrl)
    this._httpClient.get<TileDataUpdate>(requestUrl).subscribe(
      (tiles: TileDataUpdate) => this.addTiles(tiles),
      (err: HttpResponse) => { if(err.statusCode == HttpStatusCode.NotFound) console.log("There was no updates to fetch"); else console.error(err) }
    )
  }
}
