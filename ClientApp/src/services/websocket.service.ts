import { Injectable } from '@angular/core';
import * as Rx from 'rxjs'
import { webSocket } from 'rxjs/webSocket'
import { environment } from 'src/environments/environment';
import { TileData } from 'src/models/tileData';
// import { Url } from 'url';

@Injectable({
  providedIn: 'root'
})
export class WebSocketService {

  private wsConnection = webSocket(environment.WEBSOCKET_URL)
  private tileDataUpdates = new Rx.Subject<TileData>()

  constructor() {
    this.wsConnection.subscribe(this.handleWSMessage)
  }

  public subscribeToTileData(nextCallback: ((value: TileData) => void)): void {
    this.tileDataUpdates.subscribe(nextCallback)
  }

  private handleWSMessage(mess: any): void {
    this.tileDataUpdates.next(mess)
  }
}
