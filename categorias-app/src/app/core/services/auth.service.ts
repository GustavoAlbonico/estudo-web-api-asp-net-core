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
  private _accessToken = signal<string | null>(null);

  isAuthenticated = computed(() => !!this._accessToken());

  login(data: LoginRequest) {
    return this.httpClient.post<LoginResponse>(`${this.API_ENDPOINT_AUTH}/login`, data, { withCredentials: true })
  }

  refresh() {
    return this.httpClient.post<{ accessToken: string }>(`${this.API_ENDPOINT_AUTH}/refresh-token`, {}, { withCredentials: true });
  }

  setAccessToken(token: string) {
    this._accessToken.set(token);
  }

  accessToken() {
    return this._accessToken();
  }

  logout() {
    this._accessToken.set(null);
  }
}
