import { Component, OnInit } from '@angular/core';
import { Constants } from 'app/constants';

@Component({
  selector: 'app-welcome',
  templateUrl: './welcome.component.html',
  styleUrls: ['./welcome.component.scss']
})
export class WelcomeComponent implements OnInit {

  public username: string;
  
  constructor() { 
    
  }

  ngOnInit() {
    this.username = localStorage.getItem(Constants.Username);
  }

}
