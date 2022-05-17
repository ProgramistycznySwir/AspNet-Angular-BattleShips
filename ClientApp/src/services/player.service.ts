import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { v4 as uuidv4 } from 'uuid';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
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
    if(this.haveAccount)
      this.fetchPlayer();
  }

  private readonly COOKIE_NAME: string = "BattleShips-PlayerID"
  private playerIDFromCookie: string
  private _player: BehaviorSubject<Player> = new BehaviorSubject<Player>(null!)
  get player(): BehaviorSubject<Player> { return this._player as BehaviorSubject<Player> }

  private _isWaiting: boolean = false
  get isWaiting(): boolean { return this._isWaiting; }

  get haveAccount(): boolean {
    //TODO: Uhh, do this in some sane way, cause i'm too lazy for now.
    if(this.playerIDFromCookie)
      return true;
    return false;
  }

  public createPlayer(): BehaviorSubject<Player> {
    console.info("Creating player...");
    if(this.playerIDFromCookie)
      console.error("Player already have an acount!")
    if(this._isWaiting)
      console.warn("Already waiting for response from server!")
    let request = this._httpClient.post<Player>(`${environment.API_ENDPOINT}Player`, null, environment.HTTP_CREDENTIALS)
        .pipe(tap(console.info))
        .pipe(tap(_ => this._isWaiting = false))
        .subscribe(
          res => { this._player.next(res as Player); this.playerIDFromCookie= res.id; this._cookieService.set(this.COOKIE_NAME, res.id)},
          err => console.error(err)
        )
    this._isWaiting = true;
    return this._player
  }

  // For fetching data about player with ID.
  private fetchPlayer(): void  {
    // if(this.player) {
    //   console.log()
    // }
    console.info("Fetching player...");
    this._httpClient.get<Player>(`${environment.API_ENDPOINT}Player/${this.playerIDFromCookie}`)
        .pipe(res => { console.info(res); return res})
        .subscribe(
          res => this._player.next(res as Player),
          err => console.error(err)
        )
  }
  public logIn(id: string): void  {
    console.info(`Logging in with id: ${id}`);
    // if(!AppSettings.UUID_REGEX.test(id)) {
    //   console.log("I'm going insane!!!")
    // }
    // if(!AppSettings.UUID_REGEX.test(id)) {
    //   console.error("Invalid UUID", id, !AppSettings.UUID_REGEX.test(id))
    //   return;
    // }
    if(this._isWaiting) {
      console.warn("Already waiting for response from server!")
      return;
    }
    let request = this._httpClient.get<Player>(`${environment.API_ENDPOINT}Player/${id}`, environment.HTTP_CREDENTIALS)
        .pipe(res => { console.info(res); return res})
        .pipe(res => { this._isWaiting = false; return res})
        .subscribe(
          res =>  { this._player.next(res as unknown as Player); this.playerIDFromCookie= (res as any).id; this._cookieService.set(this.COOKIE_NAME, (res as any).id); console.log(res) },
          err => { console.error(err); this._isWaiting = false }
        )
    this._isWaiting = true;
  }
  // For fetching data about other players with publicID.
  public getOtherPlayer(publicID: string): Observable<Player>  {
    if(!AppSettings.UUID_REGEX.test(publicID))
      throw Error("Invalid UUID")
    return this._httpClient.get<Player>(`${environment.API_ENDPOINT}OtherPlayer/id`)
        .pipe(res => { console.info(res); return res})
  }
}