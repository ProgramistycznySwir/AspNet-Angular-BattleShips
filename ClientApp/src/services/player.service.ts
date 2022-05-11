import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { v4 as uuidv4 } from 'uuid';
import { Observable } from 'rxjs';
import { Player } from 'src/models/player';
import { CookieService } from 'ngx-cookie-service';
import { AppSettings } from 'src/AppSettings';

@Injectable({
  providedIn: 'root'
})
export class PlayerService {

  constructor(private _httpClient: HttpClient, private _cookieService: CookieService ) {
    this.playerIDFromCookie = _cookieService.get("BattleShips-PlayerID")
  }

  private readonly COOKIE_NAME: string = "BattleShips-PlayerID"
  private playerIDFromCookie: string

  // For fetching data about player with ID.
  public getPlayer(id: string): Observable<Player>  {
    return this._httpClient.get(`${AppSettings.API_ENDPOINT}`)
  }
  // For fetching data about other players with publicID.
  public getOtherPlayer(publicID: string): Observable<Player>  {

    // let myuuid = uuidv4();

  }
}