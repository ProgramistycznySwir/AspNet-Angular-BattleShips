// This file can be replaced during build by using the `fileReplacements` array.
// `ng build` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

import { HttpHeaders } from "@angular/common/http";
import { AppSettings } from "src/AppSettings";

const BACK_END_URL: string = 'localhost:7168/';
// const BACK_END_URL: string =  'echo.websocket.org/';

export const environment: any = {
  production: false,

	BACK_END_URL: BACK_END_URL,
	API_ENDPOINT: `https://${BACK_END_URL}`,
  WEBSOCKET_URL: `ws://${BACK_END_URL}`,

  HTTP_CREDENTIALS: {
    mode: 'cors',
    headers: new HttpHeaders({
      'Access-Control-Allow-Origin':'*',
      // 'Access-Control-Allow-Methods': 'POST, GET, OPTIONS'
  })}
};

/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/plugins/zone-error';  // Included with Angular CLI.
