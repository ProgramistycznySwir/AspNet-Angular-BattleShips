import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AppSettings } from 'src/AppSettings';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { WebSocketService } from './websocket.service';
import { environment } from 'src/environments/environment';
import { Game } from 'src/models/game';
import { TileData } from 'src/models/tileData';

@Injectable({
  providedIn: 'root'
})
export class GameService {

  game: BehaviorSubject<Game> = new BehaviorSubject<Game>(null!)
  tiles: BehaviorSubject<TileData[]> = new BehaviorSubject<TileData[]>(null!)

  constructor(private _httpClient: HttpClient, private _webSocketService: WebSocketService) {
    // _httpClient.get(`${environment.API_ENDPOINT}`)
    // this.tiles = <Subject<TileData[]>>_webSocketService
    //     .connect(environment.WEBSOCKET_URL)
    //     .map((res: MessageEvent): TileData => {
    //       let data = JSON.parse(res.data)
    //       return data
    //     })
  }



  public getGame(gameID: string, perspectiveID: string): Observable<Game> {
    if(!AppSettings.UUID_REGEX.test(gameID))
      throw Error("Invalid UUID")
    if(!AppSettings.UUID_REGEX.test(perspectiveID))
      throw Error("Invalid UUID")
      
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
}
