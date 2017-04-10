import { Component, OnInit, Input } from '@angular/core';
import { Router } from '@angular/router';
import { RegisterService } from './register.service';
import { Constants } from 'app/constants';
import { User } from './User';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
  providers: [
    RegisterService
  ]
})
export class RegisterComponent implements OnInit {

  private _username: string;
  private _password: string;
  private _passwordConfirm: string;
  private _firstName: string;
  private _email: string;

  private _hideSummaryError: boolean;
  private _hideUsernameError: boolean;
  private _hidePasswordError: boolean;
  private _hidePasswordConfirmError: boolean;
  private _hideFirstNameError: boolean;
  private _hideEmailError: boolean;

  constructor(private router: Router, private registerService: RegisterService) {
    this._hidePasswordError = true;
    this._hidePasswordConfirmError = true;
    this._hideSummaryError = true;
    this._hideUsernameError = true;
    this._hideFirstNameError = true;
    this._hideEmailError = true;
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
    else if (target.name === 'PasswordConfirm') {
      (this._passwordConfirm === '' && this._password !== this._passwordConfirm) ? this._hidePasswordConfirmError = false : this._hidePasswordConfirmError = true;
    }
    else if (target.name === 'FirstName') {
      this._password === '' ? this._hidePasswordError = false : this._hidePasswordError = true;
    }
    else if (target.name === 'Email') {
      this._password === '' ? this._hidePasswordError = false : this._hidePasswordError = true;
    }
  }

  onRegister() {
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
    else if (this._passwordConfirm === '' || this._passwordConfirm === undefined || this._password !== this._passwordConfirm) {
      this._hidePasswordConfirmError = false;
    }    
    else if (this._firstName === '' || this._firstName === undefined) {
      this._hideFirstNameError = false;
    }
    else if (this._email === '' || this._email === undefined) {
      this._hideEmailError = false;
    }
    else {
      let user = new User();
      user.UserName = this._username;
      user.Password = this._password;
      user.FirstName = this._firstName;
      user.Email = this._email;

      this.registerService.register(user)
        .subscribe(result => {
          if (result === true) {
            this.router.navigate(['/welcome']);
          }
          else {
            this._hideSummaryError = false;
          }
        })
    }
  }

}
