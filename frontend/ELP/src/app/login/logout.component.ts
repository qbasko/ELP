import { Component, OnInit } from '@angular/core';
import {Constants} from 'app/constants';
import { Router } from '@angular/router';

@Component({
  selector: 'app-logout',
  templateUrl: './logout.component.html',
  styleUrls: ['./logout.component.scss']
})
export class LogoutComponent implements OnInit {

  constructor(private router: Router) { }

  ngOnInit() {
    localStorage.removeItem(Constants.Username);
    this.router.navigate(['/welcome']);
  }

}
