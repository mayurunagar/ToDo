import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable()
export class AccountService {
  constructor(private http: HttpClient, @Inject('BASE_URL') private url: string) { }

  api = `${this.url}api/account`;

  public login(model: any): Observable<any> {
    return this.http.post<any>(`${this.api}/login`, model);
  }

  public register(model: any): Observable<any> {
    return this.http.post<any>(`${this.api}/register`, model);
  }
}
