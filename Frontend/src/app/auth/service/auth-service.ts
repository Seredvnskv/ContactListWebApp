import {Injectable, signal} from '@angular/core';
import {catchError, Observable, of, tap} from 'rxjs';
import {LoginDto} from '../dto/login-dto';
import {HttpClient} from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(private http: HttpClient) {}

  currentUser = signal<string | null>(null);

  login(dto: LoginDto): Observable<{ message: string }> {
    return this.http.post<{ message: string }>('/api/auth/login', dto, {
      withCredentials: true
    }).pipe(
      tap(() => this.currentUser.set(dto.email))
    );
  }

  logout(): Observable<{ message: string }> {
    return this.http.post<{ message: string }>('/api/auth/logout', {}, {
      withCredentials: true
    }).pipe(
      tap(() => this.currentUser.set(null))
    );
  }

  restoreSession(): Observable<{ isAuthenticated: boolean; email: string | null }> {
    return this.http.get<{ isAuthenticated: boolean; email: string | null }>('/api/auth/me', {
      withCredentials: true
    }).pipe(
      tap(res => this.currentUser.set(res.email)),
      catchError(() => {
        this.currentUser.set(null);
        return of({ isAuthenticated: false, email: null });
      })
    );
  }
}
