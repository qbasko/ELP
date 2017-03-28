import { Injectable } from '@angular/core';
import { Http, Headers, Response, Request, RequestOptions, RequestMethod } from '@angular/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';
import { Constants } from 'app/constants';

@Injectable()
export class LoginService {

  public token: string;

  private _signInAction ='login/signin';

  constructor(private http: Http) {
     var currentUser = JSON.parse(localStorage.getItem(Constants.CurrentUser));
     this.token = currentUser && currentUser.token;
   }

    signIn(username: string, password: string): Observable<boolean> {

    let userCredentials = {
      'Username': username,
      'Password': password
    };

    let headers = new Headers();
    headers.append('Content-Type', 'application/json; charset=utf-8');
    headers.append('Accept', 'application/json; charset=utf-8');
    let body = JSON.stringify(userCredentials);

    console.log(Constants._apiUrl.concat(this._signInAction));

    return this.http.post(Constants._apiUrl.concat(this._signInAction), body, { headers: headers })
      .map((response: Response) => {
        console.log(response.json());
        let token = response.json() && response.json().Token;
        if (token) {
          console.log("token set");
          this.token = token;
          localStorage.setItem(Constants.CurrentUser, JSON.stringify({ Username: username, Token: token }));
          return true;

        } else {
          return false;
        }
      });
  }

}
