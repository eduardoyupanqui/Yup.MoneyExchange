import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { environment } from "src/environments/environment";
import { map, Observable } from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  constructor(private httpClient: HttpClient) { }

  login(user: string, password: string): Observable<{ token: string}> {
    return this.httpClient
      .post<{ token: string}>(environment.urlService + "login/signin", { user, password })
      .pipe(
        map(response => {
          sessionStorage.setItem("token", response.token);
          return response;
        })
      );
  }

  isAuthenticated() {
    return !(sessionStorage.getItem("token") === null);
  }
}
