import { Component } from '@angular/core';
import { Form, FormControl, FormGroup, Validators } from '@angular/forms';
import { Player } from 'src/models/player';
import { UUID_Validator } from 'src/models/uuidValidator';
import { GameService } from 'src/services/game.service';
import { PlayerService } from 'src/services/player.service';

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
})
export class GameComponent {

  logIn_formGroup: FormGroup
  createGame_formGroup: FormGroup

  get haveAccount(): boolean { return this._playerService.haveAccount }
  player: Player = null!
  get isWaitingForPlayerData(): boolean { return this._playerService.isWaiting; }

  isPrivateIDRevealed: boolean = false
  public toggleRevealPublicID() { this.isPrivateIDRevealed= !this.isPrivateIDRevealed }

  constructor(private _playerService: PlayerService, private _gameService: GameService) {
    _playerService.player.subscribe( next => this.player = next )
    this.logIn_formGroup = new FormGroup({
        playerID: new FormControl('', [Validators.required, UUID_Validator()]),
      })
    this.createGame_formGroup = new FormGroup({
      otherPlayerID: new FormControl('', [UUID_Validator()]),
    })
  }

  public createAccount() {
    this._playerService.createPlayer().subscribe(
      next => this.player = next,
      err => console.error(err)
    )
  }

  public onLogIn() {
    if(this.logIn_formGroup.invalid)
      return;
    let uuid = (this.logIn_formGroup.value.playerID as string).toLowerCase()
    this._playerService.logIn(uuid)
  }

  public onCreateGame() {
    if(this.createGame_formGroup.value.otherPlayerID && this.createGame_formGroup.invalid)
      return;
    let uuid = (this.createGame_formGroup.value.otherPlayerID as string)?.toLowerCase()
    if(!uuid)
      uuid = null!;
    this._gameService.createGame(this.player.publicID, uuid)
  }
}
