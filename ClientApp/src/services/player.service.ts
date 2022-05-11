import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { v4 as uuidv4 } from 'uuid';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Player } from 'src/models/player';
import { CookieService } from 'ngx-cookie-service';
import { AppSettings } from 'src/AppSettings';

@Injectable({
  providedIn: 'root'
})
export class PlayerService {

  constructor(private _httpClient: HttpClient, private _cookieService: CookieService) {
    this.playerIDFromCookie = _cookieService.get("BattleShips-PlayerID")
  }

  private readonly COOKIE_NAME: string = "BattleShips-PlayerID"
  private playerIDFromCookie: string

  // For fetching data about player with ID.
  public getPlayer(id: string): Observable<Player>  {
    if(!AppSettings.UUID_REGEX.test(id))
      throw Error("Invalid UUID")
    var request: Observable<Player>
    if(!this.playerIDFromCookie)
      request = this._httpClient.post<Player>(`${AppSettings.API_ENDPOINT}Player`, null) as unknown as Observable<Player>
    request = this._httpClient.get<Player>(`${AppSettings.API_ENDPOINT}Player/id`)
    return request.pipe(res => { console.info(res); return res})
  }
  // For fetching data about other players with publicID.
  public getOtherPlayer(publicID: string): Observable<Player>  {
    if(!AppSettings.UUID_REGEX.test(publicID))
      throw Error("Invalid UUID")
    return this._httpClient.get<Player>(`${AppSettings.API_ENDPOINT}OtherPlayer/id`)
        .pipe(res => { console.info(res); return res})
  }
}