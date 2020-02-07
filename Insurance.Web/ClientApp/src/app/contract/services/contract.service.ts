import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { AddContract, Contract, Part } from '../models/contract.model';

@Injectable({
  providedIn: 'root'
})
export class ContractService {
  myApiUrl: string;

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json; charset=utf-8'
    })
  };

  constructor(private http: HttpClient) {
    this.myApiUrl = environment.appUrl + '/Contract/';
  }

  getContracts(): Observable<Contract[]> {
    return this.http.get<Contract[]>(this.myApiUrl)
      .pipe(
        retry(1),
        catchError(this.errorHandler)
      );
  }

  getParts(): Observable<Part[]> {
    return this.http.get<Part[]>(this.myApiUrl + "Parts")
      .pipe(
        retry(1),
        catchError(this.errorHandler)
      );
  }

  establishContract(model): Observable<AddContract> {
    return this.http.post<AddContract>(this.myApiUrl + "Establish", JSON.stringify(model), this.httpOptions)
      .pipe(
        retry(1),
        catchError(this.errorHandler)
      );
  }

  terminateContract(model): Observable<Contract> {
    return this.http.post<Contract>(this.myApiUrl + "Terminate", JSON.stringify(model), this.httpOptions)
      .pipe(
        retry(1),
        catchError(this.errorHandler)
      );
  }

  findShortestPath(model): Observable<Part[]> {
    let params = new HttpParams()
      .set('fromId', model.fromId)
      .set('toId', model.toId);

    return this.http.get<Part[]>(this.myApiUrl + "FindShortestPath", { params })
      .pipe(
        retry(1),
        catchError(this.errorHandler)
      );
  }

  errorHandler(error) {
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {
      errorMessage = error.error.message;
    } else if (error.error.hasOwnProperty('message')) {
      errorMessage = error.error.message;
    } else {
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }
    console.log(errorMessage);
    alert(errorMessage);
    return throwError(errorMessage);
  }
}
