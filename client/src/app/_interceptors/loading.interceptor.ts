import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { BusyService } from '../_services/busy.service';
import { delay, finalize } from 'rxjs/operators';

@Injectable()
export class LoadingInterceptor implements HttpInterceptor {
  constructor(private busyService: BusyService) {}

  intercept(
    request: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<unknown>> {
    // obavezno regiustrujmo ovaj interceptor u nas app modul-e
    this.busyService.busy();
    return next.handle(request).pipe(
      delay(1000), // za svaki zahtjev dodat cemo lazno kasnjenje (fake delay) koristeci operatoe iz rxjs/operators';
      finalize(() => this.busyService.idle())
    );
  }
}
