import { HttpClient /*, HttpHeaders*/ } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
//import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Member } from '../_models/member';
import { of } from 'rxjs';

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
  members: Member[] = [];

  constructor(private http: HttpClient) {}

  getMembers() /*: Observable<Member[]>*/ {
    if (this.members.length > 0) return of(this.members);
    return this.http
      .get<Member[]>(this.baseUrl + 'users' /*, httpOptions*/) // ovo ce da vrati observable Observable<Member[]> , ne treba vise httpOptions jer je implementirat interceptor za zahtjeve na koji se dodaje token ako je korisnik autentifikovan
      .pipe(
        map((members) => {
          this.members = members;
          return members;
        })
      );
  }

  //The of Operator is a creation Operator. Creation Operators are functions that create an Observable stream from a source.
  //The of Operator will create an Observable that emits a variable amount of values in sequence, followed by a Completion notification

  getMember(username: string | null) {
    const member = this.members.find((x) => x.username === username);
    if (member !== undefined)
      //jer find ako ne nadje sta treba, onda vrati undefined, pa smo stavili undefined a ne null
      return of(member);
    return this.http.get<Member>(
      this.baseUrl + 'users/' + username /*,
      httpOptions*/
    );
  }

  updateMember(member: Member) {
    return this.http.put<Member>(this.baseUrl + 'users', member).pipe(
      map(() => {
        const index = this.members.indexOf(member);
        this.members[index] = member; // jer primjenjujemo reaktivno programiranje pomocu RxJS,pa da odma u nizu promijenmo vrijednost, jer smo uveli atribut 'members: Member[] = []';
      })
    );
  }

  setMainPhoto(photoId: number) {
    return this.http.put(this.baseUrl + 'users/set-main-photo/' + photoId, {});
  }

  deletePhoto(photoId: number) {
    return this.http.delete(this.baseUrl + 'users/delete-photo/' + photoId);
  }
}
