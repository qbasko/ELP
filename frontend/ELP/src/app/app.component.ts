import { Component, OnInit } from '@angular/core';
import { Constants } from 'app/constants';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})


export class AppComponent implements OnInit {
  title = 'EnjoyLifePlanner';

  public greeting: string;
  public isLoggedIn: boolean;

  constructor() {

  }

  ngOnInit() {

    if (localStorage.getItem(Constants.Username)!=null)
    {
      this.greeting = "Hello " + localStorage.getItem(Constants.Username) +" !";
      this.isLoggedIn = true;
    }
    else{
      this.greeting = "";
      this.isLoggedIn = false;
    }
  }
}
