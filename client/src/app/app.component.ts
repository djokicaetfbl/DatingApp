import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'The Dating app';
  users: any;

  constructor(private http: HttpClient) {} // po defaut-u je asinhron
  
  ngOnInit() {
    this.getUsers();
  }

  getUsers() {
    this.http.get('https://localhost:5001/api/users').subscribe( response => {  // bez subscribe observable koji vraca http.get nece da radi nista, tek nakon subscribe dobit cemo nase podatke
          this.users = response;
    }, error => {
      console.log(error);
    }); 
  }
}
