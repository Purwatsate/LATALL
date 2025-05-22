import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { catchError, Observable, tap, throwError } from 'rxjs';
import { AuthResponse, Credentials } from '../../../shared/models/auth.model';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private readonly TOKEN_KEY = 'authToken';
  private readonly USER_KEY = 'currentUser';
  private readonly REFRESH_TOKEN_KEY = 'refreshToken';

  private http = inject(HttpClient);
  private router = inject(Router);

  constructor() {}

  login(credentials: Credentials): Observable<AuthResponse> {
    const loginUrl = 'http://localhost:3000/api/login';

    return this.http.post<AuthResponse>(loginUrl, credentials).pipe(
      tap((response) => {
        this.storeToken(response.token);
        if (response.refreshToken) {
          this.storeRefreshToken(response.refreshToken);
        }
        if (response.user) {
          this.storeUser(response.user);
        }
        // Update authentication status
        // this._isAuthenticated.next(true);
        // this._currentUser.next(response.user || null);
        console.log('Login successful!');
      }),
      catchError(this.handleError)
    );
  }

  getToken(): string | null {
    // return localStorage.getItem(this.TOKEN_KEY);
    return 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiJlMWRjMTI5Yi0yMmEyLTQ5MDktYWQ3Zi04NWY3MjE1YTA1ZDMiLCJ1bmlxdWVfbmFtZSI6IlVzZXIgIDEiLCJyb2xlIjoiVXNlciIsIm5iZiI6MTc0NzkzMzg0MiwiZXhwIjoxNzQ3OTM1NjQyLCJpYXQiOjE3NDc5MzM4NDIsImlzcyI6IkxBVEFMTCIsImF1ZCI6IkxBVEFMTEFQSUNsaWVudCJ9.ydZ5CEI0YAfOe3ZtN5n0bJC90DyKnpT3Gn3ftopioaM';
  }

  getRefreshToken(): string | null {
    return localStorage.getItem(this.REFRESH_TOKEN_KEY);
  }

  hasToken(): boolean {
    return !!this.getToken();
  }

  /**
   * Retrieves the stored user data.
   * @returns The user object or null if not found.
   */
  getStoredUser(): any | null {
    const userJson = localStorage.getItem(this.USER_KEY);
    return userJson ? JSON.parse(userJson) : null;
  }

  private storeToken(token: string): void {
    localStorage.setItem(this.TOKEN_KEY, token);
  }

  private clearToken(): void {
    localStorage.removeItem(this.TOKEN_KEY);
  }

  private storeRefreshToken(token: string): void {
    localStorage.setItem(this.REFRESH_TOKEN_KEY, token);
  }

  private clearRefreshToken(): void {
    localStorage.removeItem(this.REFRESH_TOKEN_KEY);
  }

  private storeUser(user: any): void {
    localStorage.setItem(this.USER_KEY, JSON.stringify(user));
  }

  private clearUser(): void {
    localStorage.removeItem(this.USER_KEY);
  }

  private handleError(error: HttpErrorResponse): Observable<never> {
    let errorMessage = 'An unknown error occurred!';
    if (error.error instanceof ErrorEvent) {
      // Client-side errors
      errorMessage = `Error: ${error.error.message}`;
    } else {
      // Server-side errors
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
      if (error.status === 401 || error.status === 403) {
        console.error('Authentication or Authorization error:', error.error);
      }
    }
    console.error(errorMessage);
    return throwError(() => new Error(errorMessage));
  }

  refreshToken(): Observable<AuthResponse> {
    const refreshUrl = 'http://localhost:3000/api/refresh-token';
    const currentRefreshToken = this.getRefreshToken();

    if (!currentRefreshToken) {
      this.logout(); // No refresh token, force logout
      return throwError(() => new Error('No refresh token available.'));
    }

    return this.http
      .post<AuthResponse>(refreshUrl, { refreshToken: currentRefreshToken })
      .pipe(
        tap((response) => {
          this.storeToken(response.token);
          if (response.refreshToken) {
            this.storeRefreshToken(response.refreshToken);
          }
          console.log('Token refreshed successfully!');
        }),
        catchError((error) => {
          console.error('Token refresh failed!', error);
          this.logout(); // Force logout on refresh failure
          return throwError(() => new Error('Token refresh failed.'));
        })
      );
  }

  logout(): void {
    this.clearToken();
    this.clearUser();
    this.clearRefreshToken();

    // this._isAuthenticated.next(false);
    // this._currentUser.next(null);
    this.router.navigate(['/login']);
    console.log('User logged out.');
  }
}
