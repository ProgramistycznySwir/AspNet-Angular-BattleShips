import { Injectable } from '@angular/core';
import { HubConnectionBuilder } from '@microsoft/signalr';
import * as Rx from 'rxjs'
import { webSocket } from 'rxjs/webSocket'
import { environment } from 'src/environments/environment';
import { TileData } from 'src/models/tileData';
// import { Url } from 'url';

@Injectable({
  providedIn: 'root'
})
export class WebSocketService {
  connection: any;

  constructor() {
    this.initWebSocket();
    console.warn("UHHHHH")
    this.connection.start();
  }

  ngOnInit(): void {
  }

  initWebSocket() {
    this.connection = new HubConnectionBuilder()
      .withUrl(`${environment.WEBSOCKET_URL}hub/game`)
      .build();

    this.connection.on('updateTileData', (tileData: TileData) => {
      this.tileDataUpdates.next(tileData);
    });
  }

  // private wsConnection = webSocket(environment.WEBSOCKET_URL)
  private tileDataUpdates = new Rx.Subject<TileData>()

  // constructor() {
  //   this.wsConnection.subscribe(this.handleWSMessage)
  // }

  public subscribeToTileData(nextCallback: ((value: TileData) => void)): void {
    this.tileDataUpdates.subscribe(nextCallback)
  }

  // private handleWSMessage(mess: any): void {
  //   this.tileDataUpdates.next(mess)
  // }
}
