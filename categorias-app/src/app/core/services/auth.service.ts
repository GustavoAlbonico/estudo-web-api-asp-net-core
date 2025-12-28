import { Injectable, inject, signal, computed } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { LoginRequest, LoginResponse } from '../models/auth.model';
import { environment } from '../../../environments/environment.development';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private httpClient = inject(HttpClient);

  private readonly API_ENDPOINT_AUTH = `${environment.apiAuth}/api/autoriza`;

  private _token = signal<string | null>(
    localStorage.getItem('token')
  );

  isAuthenticated = computed(() => !!this._token());

  login(data: LoginRequest) {
    return this.httpClient.post<LoginResponse>(`${this.API_ENDPOINT_AUTH}/login`, data);
  }

  setSession(response: LoginResponse) {
    this._token.set(response.token);
    localStorage.setItem('token', response.token);
  }

  logout() {
    this._token.set(null);
    localStorage.removeItem('token');
  }

  get token() {
    return this._token();
  }
}
