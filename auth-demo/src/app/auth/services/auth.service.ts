import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment as ENV } from 'src/environments/environment.development';
import { Login } from '../models/login.model';
import { Register } from '../models/register.model';
import { LoginResponse } from '../models/login-response.model';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private readonly http = inject(HttpClient);
  private readonly router = inject(Router);
  private readonly toastr = inject(ToastrService);

  private readonly baseUrl = ENV.apiBaseUrl;

  login(payload: Login) {
    return this.http.post<LoginResponse>(`${this.baseUrl}/auth/login`, payload);
  }

  register(payload: Register) {
    return this.http.post(`${this.baseUrl}/auth/register`, payload);
  }

  verify() {
    return this.http.get<boolean>(`${this.baseUrl}/auth/verify`);
  }

  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    this.toastr.success('Logout successful', 'Success');
    this.router.navigate(['/auth', 'login']);
  }
}
