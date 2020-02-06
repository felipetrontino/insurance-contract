import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Advisor } from '../models/advisor.model';

@Injectable({
  providedIn: 'root'
})
export class AdvisorService {
  myApiUrl: string;

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json; charset=utf-8'
    })
  };
  constructor(private http: HttpClient) {
    this.myApiUrl = environment.appUrl + '/Advisor/';
  }

  getAdvisors(): Observable<Advisor[]> {
    return this.http.get<Advisor[]>(this.myApiUrl)
      .pipe(
        retry(1),
        catchError(this.errorHandler)
      );
  }

  getAdvisor(id: string): Observable<Advisor> {
    return this.http.get<Advisor>(this.myApiUrl + id)
      .pipe(
        retry(1),
        catchError(this.errorHandler)
      );
  }

  addAdvisor(model): Observable<Advisor> {     
    return this.http.post<Advisor>(this.myApiUrl, JSON.stringify(model), this.httpOptions)
      .pipe(
        retry(1),
        catchError(this.errorHandler)
      );
  }

  updateAdvisor(id: string, model): Observable<Advisor> {
    return this.http.put<Advisor>(this.myApiUrl + id, JSON.stringify(model), this.httpOptions)
      .pipe(
        retry(1),
        catchError(this.errorHandler)
      );
  }

  deleteAdvisor(id: string): Observable<Advisor> {
    return this.http.delete<Advisor>(this.myApiUrl + id)
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
