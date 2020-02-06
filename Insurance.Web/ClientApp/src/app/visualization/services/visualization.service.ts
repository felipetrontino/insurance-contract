import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Node, Edge } from '../models/visualization.model';

@Injectable({
  providedIn: 'root'
})
export class VisualizationService {
  myApiUrl: string;

  constructor(private http: HttpClient) {
    this.myApiUrl = environment.appUrl + '/Contract/';
  }

  getNodes(): Observable<Node[]> {
    return this.http.get<Node[]>(this.myApiUrl + "Nodes")
      .pipe(
        retry(1),
        catchError(this.errorHandler)
      );
  }

  getEdges(): Observable<Edge[]> {
    return this.http.get<Edge[]>(this.myApiUrl + "Edges")
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
