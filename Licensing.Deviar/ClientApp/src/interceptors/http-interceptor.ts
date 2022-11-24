import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, finalize } from 'rxjs/operators';
import { Router } from '@angular/router';

@Injectable()
export class TokenizedInterceptor implements HttpInterceptor {
    constructor(private router: Router) { }

    // Add authorization token to the request
    private setHeaders(request: HttpRequest<any>) {
        const token = localStorage.getItem('token');
        if (token) {
            request = request.clone({
                setHeaders: {
                    Authorization: `Bearer ${token}`
                }
             });
        }
        return request;
    }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        request = this.setHeaders(request);
        return next.handle(request)
            .pipe(
            catchError((error: any) => {
                 if (error instanceof HttpErrorResponse) {
                    if (error.status == 401) {
                        localStorage.removeItem('token');
                        this.router.navigate(['/login']);
                    }

                    switch (error.status) {
                        case 0:
                            return throwError( {status: error.status, message: 'Unable to connect to the server. Please try again later.' } as AppResponse );
                        case 400:
                        case 401:
                        case 403:
                        case 404:
                        case 500:
                        default:
                            return throwError({status: error.status, message: error.message, data: error.error } as AppResponse );
                    }
                } else if (error.error instanceof ErrorEvent) { // Client Side Error
                    return throwError({status: error.status, message: error.error.message } as AppResponse);
                } else {  // Server Side Error
                    return throwError({status: error.status, message: error.error.message } as AppResponse);
                }

             }),
             finalize(() => {
                // do something at the end
             })
          );
    }
}
export interface AppResponse {
    status: number;
    message: string;
    data: object;
}
