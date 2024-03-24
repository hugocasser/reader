import { Injectable, OnInit, inject } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { catchError, map } from 'rxjs/operators';
import { throwError } from 'rxjs';
import { User, UserItem } from '../models/user';
import { enviroment } from './enviroment/enviroment'; // Import your User model if applicable
import { AuthTokens } from '../models/auth-token';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private baseUrl = enviroment.Identity;
  private refreshTokenKey = 'refreshToken';
  private userIdKey = 'userId';
  private JwtTokenKey = 'token';
  private errors = '';
  private http = inject(HttpClient)

  register(user: User): string | boolean {
    let y = this.http.post(this.baseUrl + '/register', user)
      .pipe(catchError(this.handleError));

    if (this.errors !== '') {
      return this.errors;
    }

    return true;
  }

  login(email: string, password: string): string | boolean {
    this.http.post<AuthTokens>(
      this.baseUrl + 'auth/login', { email, password })
      .subscribe({
        next: (tokens) => {
          localStorage.setItem('token', tokens.token);
          localStorage.setItem('refreshToken', tokens.refreshToken);
          localStorage.setItem('userId', tokens.id);
        },
        error: (error) => {
          return this.errors;
        },
        complete: () => { }
      });

    return true
  }

  refreshToken(): boolean {
    const refreshToken = localStorage.getItem('refreshToken');
    const userId = localStorage.getItem('userId');
    this.http.post<{ id: string, token: string, refreshToken: string }>(`${this.baseUrl}/refresh-token`, { userId, refreshToken })
      .pipe(
        map(response => {
          this.storeTokens(response);
          return true;
        }),
        catchError((error: any) => {
          return error;
        }
          ));

    return false;
  }

  isLoginedIn(): boolean {
    var refreshToken = localStorage.getItem(this.refreshTokenKey)

    if (refreshToken === null) {
      return false;
    }

    let isRefreshSuccsess = this.refreshToken();

    if (isRefreshSuccsess === true) {
      return true;
    }

    return false;
  }

  getJwtToken() {
    return localStorage.getItem('jwtToken');
  }

  getUserId() {
    return localStorage.getItem('userId');
  }

  logout() {
    localStorage.removeItem('jwtToken');
    localStorage.removeItem('userId');
    localStorage.removeItem('refreshToken');
  }

  deleteUser(id: string): string | boolean {
    this.http.delete(`${this.baseUrl}/users/${id}`)
      .pipe(catchError(this.handleError));
    return this.errors;
  }

  getUsers(pageSize: number, page: number): UserItem[] {
    let items : UserItem[] = [];
    this.http.get<UserItem[]>(`${this.baseUrl}/users?page=${page}&pageSize=${pageSize}`)
      .subscribe({
        next: (users) => {
          items = users;
        },
        error: (error) => {
          this.handleError(error);
        },
        complete: () => {}
      });

      return items;
  };

  private storeTokens(tokens: { id: string, token: string, refreshToken: string }) {
  localStorage.setItem(this.userIdKey, tokens.id.toString());
  localStorage.setItem(this.JwtTokenKey, tokens.token);
  localStorage.setItem(this.refreshTokenKey, tokens.refreshToken);
}

  private handleError(error: any) {
  if (error instanceof HttpErrorResponse) {
    this.errors = error.status.toString();
    return throwError(() => new Error(error.status.toString()));
  }

  this.errors = 'error'

  return throwError(() => new Error(error));
}

clearErrors(){
  this.errors = '';
}
}

