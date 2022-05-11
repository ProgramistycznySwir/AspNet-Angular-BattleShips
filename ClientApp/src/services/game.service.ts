import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class GameService {

  constructor(private _httpClient: HttpClient) { }

  public getGame(gameID: string) {
    
  }
}
