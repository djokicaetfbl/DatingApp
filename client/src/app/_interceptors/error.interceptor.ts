import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { catchError } from 'rxjs/operators';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  // potrebno ga je registrovati u providers u app.module.ts

  constructor(private router: Router, private toastr: ToastrService) {}

  intercept(
    request: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      // sa pipe dodajemo funkcionalnosti nasem interceptoru, konkretno implementirat cemo error-e za http zahtjeve koji dolaze i odlze sa klijenta
      catchError((error) => {
        if (error) {
          switch (error.status) {
            case 400:
              if (error.error.errors) {
                // u console-i kad pristupamo JSON objektu pa po dubini
                const modalStateErrors = [];
                for (const key in error.error.errors) {
                  if (error.error.errors[key]) {
                    modalStateErrors.push(error.error.errors[key]);
                  }
                }
                throw modalStateErrors.flat(); // ukljucimo es2019 u tsconfig.json
              } else {
                this.toastr.error(
                  error.statusText,
                  error.status + ' - Bad Request'
                );
              }
              break;

            case 401:
              this.toastr.error(
                error.statusText,
                error.status + ' - Authorization'
              ); //(error.statusText, error.status);
              break;

            case 404:
              this.router.navigateByUrl('/not-found'); // za ovaj error prebaci nas na drugu rutu
              break;

            case 500:
              const navigationExtras: NavigationExtras = {
                state: { error: error.error },
              };
              this.router.navigateByUrl('/server-error', navigationExtras); // slanje podataka u drugu komponentu putem rute, za to se koristi NavigationExtras
              break;
            default:
              this.toastr.error('Somethning unexpected went wrong');
              console.log(error);
              break;
          }
        }
        return throwError(error);
      })
    );
  }
}
