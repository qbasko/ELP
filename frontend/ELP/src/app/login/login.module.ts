import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import {LoginService} from './login.service';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [],
  providers: [
    LoginService
  ]
})
export class LoginModule { }
