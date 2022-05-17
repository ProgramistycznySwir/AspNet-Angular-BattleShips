import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
// import { HubConnectionBuilder } from '@microsoft/signalr';
import * as Rx from 'rxjs'
import { webSocket } from 'rxjs/webSocket'
import { environment } from 'src/environments/environment';
import { TileData } from 'src/models/tileData';
import { PlayerService } from './player.service';
// import { Url } from 'url';

@Injectable({
  providedIn: 'root'
})
export class WebSocketService {
  private  connection: any = new signalR.HubConnectionBuilder()
      .withUrl(`${environment.WEBSOCKET_URL}hub/game`, {skipNegotiation: true, transport: signalR.HttpTransportType.WebSockets})
      .configureLogging(signalR.LogLevel.Information)
      .build();


  constructor(private _playerService: PlayerService) { 
    this.connection.onclose(async () => {
      await this.start();
    });
    this.connection.on("updateTileData", (data: TileData) => this.tileDataUpdates.next(data));
    this.start();
  }

  public async start() {
    try {
      await this.connection.start();
      this.connection.send()
      console.log("Connected to WebSockets!");
    } catch (err) {
      console.log(err);
      setTimeout(() => this.start(), 5000);
    }
  }

  private tileDataUpdates = new Rx.Subject<TileData>()
  public subscribeToTileData(nextCallback: ((value: TileData) => void)): void {
    this.tileDataUpdates.subscribe(nextCallback)
  }
}
