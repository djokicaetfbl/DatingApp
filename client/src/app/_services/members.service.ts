import { HttpClient /*, HttpHeaders*/ } from '@angular/common/http';
import { Injectable } from '@angular/core';
//import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Member } from '../_models/member';

/*const httpOptions = {
  headers: new HttpHeaders({
    Authorization:
      'Bearer ' + JSON.parse(localStorage.getItem('user') as any)?.token, // pazi da je Bearer pa space pa +JSON... , pazi na upitnik, jer kada nema token tada je null pa ce puci program
  }),
};*/ // kortstili smo kada nije bio implementirat JwtInterceptor

@Injectable({
  providedIn: 'root',
})
export class MembersService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getMembers() /*: Observable<Member[]>*/ {
    return this.http.get<Member[]>(this.baseUrl + 'users' /*, httpOptions*/); // ovo ce da vrati observable Observable<Member[]> , ne treba vise httpOptions jer je implementirat interceptor za zahtjeve na koji se dodaje token ako je korisnik autentifikovan
  }

  getMember(username: string | null) {
    return this.http.get<Member>(
      this.baseUrl + 'users/' + username /*,
      httpOptions*/
    );
  }
}
