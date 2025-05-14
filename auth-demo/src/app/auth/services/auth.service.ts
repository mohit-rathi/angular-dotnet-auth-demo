import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment as ENV } from 'src/environments/environment.development';
import { Login } from '../models/login.model';
import { Register } from '../models/register.model';
import { LoginResponse } from '../models/login-response.model';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private readonly http = inject(HttpClient);

  private readonly baseUrl = ENV.apiBaseUrl;

  login(payload: Login) {
    return this.http.post<LoginResponse>(`${this.baseUrl}/auth/login`, payload);
  }

  register(payload: Register) {
    return this.http.post(`${this.baseUrl}/auth/register`, payload);
  }
}
