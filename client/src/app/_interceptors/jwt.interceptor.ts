import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { AccountService } from '../_services/account.service';
import { User } from '../_models/user';
import { take } from 'rxjs/operators';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
  constructor(private accountService: AccountService) {}

  intercept(
    request: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<unknown>> {
    let currentUser: User | undefined;
    this.accountService.currentUser$
      .pipe(take(1))
      .subscribe((user) => (currentUser = user)); // kako smo sa pipe(take(1)) uzeli jedan (posao zavrsen), onda nam nije potreban unsubscribe jer smo posao zavrsili i odjavili smo se.

    if (currentUser) {
      //ako posotji user, onda klonirajmo request i dodajmo mui autentifikaciju
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${currentUser.token}`, // be zaboraci space nakon Bearer !!
        },
      });
    } // potrebno je interceptor obezbijediti (provide) unutar app.module-a

    return next.handle(request);
  }
}
