import { HttpErrorResponse, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { catchError, throwError } from "rxjs";

@Injectable({
  providedIn: 'root',
})
export class AuthHtppInterceptor implements HttpInterceptor 
{
    constructor(private router: Router) { }
    intercept(req: HttpRequest<any>, next: HttpHandler) 
    {
        const token: string | null = sessionStorage.getItem('token');
        if (token) {
            req = req.clone({
              setHeaders: {
                authorization: `Bearer ${ token }`
              }
            })
        }
        return next.handle(req).pipe(
            catchError((err: HttpErrorResponse) => {
              if (err.status === 401) {
                this.router.navigateByUrl('/login');
              }
              return throwError(()=> err );
      
            })
          );;
    }
}
