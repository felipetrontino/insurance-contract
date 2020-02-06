import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Mga } from '../models/mga.model';

@Injectable({
  providedIn: 'root'
})
export class MgaService {
  myApiUrl: string;

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json; charset=utf-8'
    })
  };
  constructor(private http: HttpClient) {
    this.myApiUrl = environment.appUrl + '/Mga/';
  }

  getMgas(): Observable<Mga[]> {
    return this.http.get<Mga[]>(this.myApiUrl)
      .pipe(
        retry(1),
        catchError(this.errorHandler)
      );
  }

  getMga(id: string): Observable<Mga> {
    return this.http.get<Mga>(this.myApiUrl + id)
      .pipe(
        retry(1),
        catchError(this.errorHandler)
      );
  }

  addMga(model): Observable<Mga> {
    return this.http.post<Mga>(this.myApiUrl, JSON.stringify(model), this.httpOptions)
      .pipe(
        retry(1),
        catchError(this.errorHandler)
      );
  }

  updateMga(id: string, model): Observable<Mga> {
    return this.http.put<Mga>(this.myApiUrl + id, JSON.stringify(model), this.httpOptions)
      .pipe(
        retry(1),
        catchError(this.errorHandler)
      );
  }

  deleteMga(id: string): Observable<Mga> {
    return this.http.delete<Mga>(this.myApiUrl + id)
      .pipe(
        retry(1),
        catchError(this.errorHandler)
      );
  }

  errorHandler(error) {
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {
      errorMessage = error.error.message;
    } else if(error.error.hasOwnProperty('message')){
      errorMessage = error.error.message;
    } else {
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }
    console.log(errorMessage);
    return throwError(errorMessage);
  }
}
