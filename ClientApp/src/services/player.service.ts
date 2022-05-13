import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { v4 as uuidv4 } from 'uuid';
import { BehaviorSubject, Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Player } from 'src/models/player';
import { CookieService } from 'ngx-cookie-service';
import { AppSettings } from 'src/AppSettings';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PlayerService {

  constructor(private _httpClient: HttpClient, private _cookieService: CookieService) {
    this.playerIDFromCookie = _cookieService.get(this.COOKIE_NAME)
  }

  private readonly COOKIE_NAME: string = "BattleShips-PlayerID"
  private playerIDFromCookie: string
  private player: BehaviorSubject<Player> = new BehaviorSubject<Player>(null!)

  private _isWaiting: boolean = false
  get isWaiting(): boolean { return this._isWaiting; }

  public haveAccount() {
    //TODO: Uhh, do this in some sane way, cause i'm too lazy for now.
    if(this.playerIDFromCookie)
      return true;
    return false;
  }

  public createPlayer(): BehaviorSubject<Player> {
    if(this.playerIDFromCookie)
      console.error("Player already have an acount!")
    if(this._isWaiting)
      console.warn("Already waiting for response from server!")
    let request = this._httpClient.post<Player>(`${environment.API_ENDPOINT}Player`, null)
        .pipe(res => { console.info(res); return res})
        .pipe(res => { this._isWaiting = false; return res})
        .subscribe(
          res => { this.player.next(res as Player); this.playerIDFromCookie= res.id; this._cookieService.set(this.COOKIE_NAME, res.id)},
          err => console.error(err)
        )
    this._isWaiting = true;
    return this.player
  }

  public setPlayerID(id: string): void {
    if(!AppSettings.UUID_REGEX.test(id))
      throw Error("Invalid UUID")
    if(this._isWaiting)
      console.warn("Already waiting for response from server!")
    let request = this._httpClient.get<Player>(`${environment.API_ENDPOINT}Player/${id}`)
        .pipe(res => { console.info(res); return res})
        .pipe(res => { this._isWaiting = false; return res})
        .subscribe(
          res => { this.player.next(res as Player); this.playerIDFromCookie= res.id; this._cookieService.set(this.COOKIE_NAME, res.id)},
          err => console.error(err)
        )
    this._isWaiting = true;
  }
  // For fetching data about player with ID.
  // public getPlayer(id: string): Observable<Player>  {
  //   if(!AppSettings.UUID_REGEX.test(id))
  //     throw Error("Invalid UUID")
  //   var request: Observable<Player> = this._httpClient.get<Player>(`${environment.API_ENDPOINT}Player/id`)
  //   return request.pipe(res => { console.info(res); return res})
  // }
  // For fetching data about other players with publicID.
  public getOtherPlayer(publicID: string): Observable<Player>  {
    if(!AppSettings.UUID_REGEX.test(publicID))
      throw Error("Invalid UUID")
    return this._httpClient.get<Player>(`${environment.API_ENDPOINT}OtherPlayer/id`)
        .pipe(res => { console.info(res); return res})
  }
}