import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, catchError, throwError } from 'rxjs';
import { Book } from '../../shared/models/BookModel';
import { User } from '../../shared/models/UserModel';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  constructor(private http: HttpClient) {}

  endpoint = 'https://localhost:7028';

  //BOOKS
  getBooks(): Observable<Book[]> {
    return this.http.get<Book[]>('https://localhost:7028/book/get-books/').pipe(
      catchError((error) => {
        console.error('Error fetching books:', error);
        // Handle the error gracefully in your component
        throw error; // Use `throw` to rethrow the error for further handling
      })
    );
  }

  //AUTH
  login(username: string, password: string): Observable<User> {
    return this.http
      .post<User>(`${this.endpoint}/login`, {
        Username: username,
        Password: password,
      })
      .pipe(
        catchError((error) => {
          console.error(error);

          throw error;
        })
      );
  }
}
