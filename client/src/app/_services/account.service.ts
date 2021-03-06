import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { User } from '../_models/user';

//Angular servi je Singleton, sto znaci da dok god je aplikacija pokrenuta ovaj servis je dostupan.
//sa komponentama je drugacije, kako se prebacujemo sa komponente na komponentu ona se 'unistava'

@Injectable({
  // pomocu Injectable dekorata, ovaj servis moze da da bude injektovan u drugu komponentu ili u drugi servis unutar aplikacije
  providedIn: 'root', // ne moramo ga ukljuciti u provides unutar appcomponent.ts  ( u nekim starijim verzijama je moralo da se u appcomponent.ts da se ukljuci)
})
export class AccountService {
  baseUrl = environment.apiUrl; //'https://localhost:5001/api/';
  private currentUserSource = new ReplaySubject<User>(1); // ReplaySubject je Observable tip, i ocekuje jednog Usera , tj. smjestit ce samo jednog :D
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient) {}

  login(model: any) {
    return this.http.post<User>(this.baseUrl + 'account/login', model).pipe(
      map((response: User) => {
        const user = response;
        if (user) {
          this.setCurrentUser(user);
        }
      })
    );
  }

  register(model: any) {
    return this.http.post<User>(this.baseUrl + 'account/register', model).pipe(
      map((user: User) => {
        if (user) {
          this.setCurrentUser(user);
        }
        //return user; // sad smo sigurni da vraca user-a, nece u console.log za response da ispisuje undefined vec ce da ispise konkretnog user-a
      })
    );
  }

  setCurrentUser(user: User) {
    localStorage.setItem('user', JSON.stringify(user));
    this.currentUserSource.next(user);
  }

  logout() {
    localStorage.removeItem('user');
    this.currentUserSource.next(null as any); // vise nije pretplacen :D , odlicno :D
  }
}
