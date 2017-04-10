import { Component, OnInit, Input } from '@angular/core';
import { Router } from '@angular/router';
import { LoginService } from './login.service';
import { Constants } from 'app/constants';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss'],
    providers: [
        LoginService
    ]
})

export class LoginComponent implements OnInit {

    @Input() loggedUsername: string;

    private _username: string;
    private _password: string;
    private _hideSummaryError: boolean;
    private _hideUsernameError: boolean;
    private _hidePasswordError: boolean;

    constructor(private router: Router, private loginService: LoginService) {
        this._hidePasswordError = true;
        this._hideSummaryError = true;
        this._hideUsernameError = true;
    }

    ngOnInit() {
    }

    onKeyUp(event: KeyboardEvent) {
        let target = <HTMLFormElement>event.target;
        if (target.name === 'UserName') {
            this._username === '' ? this._hideUsernameError = false : this._hideUsernameError = true;
        }
        else if (target.name === 'Password') {
            this._password === '' ? this._hidePasswordError = false : this._hidePasswordError = true;
        }
    }

    onLogin() {
        if ((this._username === '' || this._username === undefined) && (this._password === '' || this._password === undefined)) {
            this._hideUsernameError = false;
            this._hidePasswordError = false;
        }
        else if (this._username === '' || this._username === undefined) {
            this._hideUsernameError = false;
        }
        else if (this._password === '' || this._password === undefined) {
            this._hidePasswordError = false;
        }
        else {

            this.loginService.signIn(this._username, this._password)
                .subscribe(result => {
                    if (result === true) {
                        localStorage.setItem(Constants.Username, this._username);
                        this.loggedUsername = this._username;
                        this.router.navigate(['/welcome']);
                    }
                    else {
                        this._hideSummaryError = false;
                    }
                })
        }
    }

}
