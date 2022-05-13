import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import {MatButtonModule} from '@angular/material/button';
import { MatInputModule } from '@angular/material/input'

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CookieService } from 'ngx-cookie-service';
import { WebSocketService } from 'src/services/websocket.service';
import { GameService } from 'src/services/game.service';
import { PlayerService } from 'src/services/player.service';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { GameComponent } from './game/game.component';
import { MatDialogModule } from '@angular/material/dialog';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    GameComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'game/:id', component: GameComponent},
    ]),
    BrowserAnimationsModule,

    MatButtonModule,
    MatInputModule,
    MatDialogModule,
  ],
  providers: [
    CookieService,
    WebSocketService,
    GameService,
    PlayerService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
