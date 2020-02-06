import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Carrier } from '../models/carrier.model';

@Injectable({
  providedIn: 'root'
})
export class CarrierService {
  myApiUrl: string;

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json; charset=utf-8'
    })
  };
  constructor(private http: HttpClient) {
    this.myApiUrl = environment.appUrl + '/Carrier/';   
  }

  getCarriers(): Observable<Carrier[]> {
    return this.http.get<Carrier[]>(this.myApiUrl)
      .pipe(
        retry(1),
        catchError(this.errorHandler)
      );
  }

  getCarrier(id: string): Observable<Carrier> {
    return this.http.get<Carrier>(this.myApiUrl + id)
      .pipe(
        retry(1),
        catchError(this.errorHandler)
      );
  }

  addCarrier(model): Observable<Carrier> {
    return this.http.post<Carrier>(this.myApiUrl, JSON.stringify(model), this.httpOptions)
      .pipe(
        retry(1),
        catchError(this.errorHandler)
      );
  }

  updateCarrier(id: string, model): Observable<Carrier> {
    return this.http.put<Carrier>(this.myApiUrl + id, JSON.stringify(model), this.httpOptions)
      .pipe(
        retry(1),
        catchError(this.errorHandler)
      );
  }

  deleteCarrier(id: string): Observable<Carrier> {
    return this.http.delete<Carrier>(this.myApiUrl + id)
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
