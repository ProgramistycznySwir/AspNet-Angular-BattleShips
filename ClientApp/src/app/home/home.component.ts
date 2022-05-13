import { Component } from '@angular/core';
import { Player } from 'src/models/player';
import { PlayerService } from 'src/services/player.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {

  haveAccount: boolean = false
  player: Player = null!
  get isWaitingForPlayerData(): boolean { return this._playerService.isWaiting; }

  constructor(private _playerService: PlayerService) {
    this.haveAccount = _playerService.haveAccount()
  }

  public createAccount() {
    this._playerService.createPlayer().subscribe(
      next => this.player = next,
      err => console.error(err)
    )
  }
}
