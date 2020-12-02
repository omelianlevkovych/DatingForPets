import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'The pets daiting app';
  pets: any;
  // Dependency injection
  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.getPets();
  }

  getPets() {
    this.http.get('https://localhost:5001/api/pets').subscribe(response => {
      this.pets = response;
    }, error => {
      console.log(error);
    });
  }
}
