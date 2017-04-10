import { Injectable } from '@angular/core';
import { Http, Headers, Response, Request, RequestOptions, RequestMethod } from '@angular/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';
import { Constants } from 'app/constants';
import {User} from './user';

@Injectable()
export class RegisterService {

 private _registerAction ='login/register';

  constructor(private http: Http) {

   }

   register(user: User): Observable<boolean>{

     let body = JSON.stringify(user);
     let headers = new Headers({ 'Content-Type': 'application/json','Accept': 'application/json' });
     let options = new RequestOptions({ headers: headers });

     return this.http.post(Constants._apiUrl.concat(this._registerAction), body, options)
     .map((response: Response)=>
     {
        console.log(response.json());
        return true;
     });
     
   }

}
