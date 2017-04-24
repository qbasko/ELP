import { Injectable } from '@angular/core';
import { Http, Headers, Response, Request, RequestOptions, RequestMethod } from '@angular/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';
import { Constants } from 'app/constants';

@Injectable()
export class LoginService {

  public token: string;

  private _signInAction = 'login/signin';
  private _loginWithGoogleAction = 'login/externalLogin';

  constructor(private http: Http) {
    var currentUser = JSON.parse(localStorage.getItem(Constants.CurrentUser));
    this.token = currentUser && currentUser.token;
  }

  signIn(username: string, password: string): Observable<boolean> {
    let userCredentials = {
      'Username': username,
      'Password': password
    };

    let body = JSON.stringify(userCredentials);
    let headers = new Headers({ 'Content-Type': 'application/json', 'Accept': 'application/json' });
    let options = new RequestOptions({ headers: headers });

    console.log(Constants._apiUrl.concat(this._signInAction));
    return this.http.post(Constants._apiUrl.concat(this._signInAction), body, options)
      .map((response: Response) => {
        //console.log(response.json());
        return true;
        // let token = response.json() && response.json().Token;
        // if (token) {
        //   console.log("token set");
        //   this.token = token;
        //   localStorage.setItem(Constants.CurrentUser, JSON.stringify({ Username: username, Token: token }));
        //   return true;

        // } else {
        //   return false;
        // }
      });
  }

  externalLogin(provider: string): Observable<boolean> {

    let data = {
      'Provider': provider,
      'RedirectUrl': "testUrl"
    };
    let body = JSON.stringify(data);

    let headers = new Headers({ 'Content-Type': 'application/json', 'Accept': 'application/json' });
    let options = new RequestOptions({ headers: headers });

    let url = "";
    if (provider == "Google") {
      url = Constants._apiUrl.concat(this._loginWithGoogleAction);
    }
    if (provider == "Facebook") {
      url = Constants._apiUrl.concat(this._loginWithGoogleAction);
    }
    
    console.log(url);

    return this.http.post(url, body, options)
      .map((response: Response) => {
        console.log(response.json());
        return true;
      });


  }

}
