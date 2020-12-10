import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { User } from './_models/user';
import { AccountService } from './_services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'The pets daiting app';
  pets: any;
  // Dependency injection
  constructor(private http: HttpClient, private accountService: AccountService) {}

  ngOnInit() {
    this.getUsers();
    this.setCurrentUser();
  }
  
  // Retreiving the current user from the local storage and setting to the account service
  setCurrentUser() {
    const user: User = JSON.parse(localStorage.getItem('user'));
    this.accountService.setCurrentUser(user);
  }

  // Retreiving the pet user list
  getUsers() {
    this.http.get('https://localhost:5001/api/pets').subscribe(response => {
      this.pets = response;
    }, error => {
      console.log(error);
    });
  }
}
