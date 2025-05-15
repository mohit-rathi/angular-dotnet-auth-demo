import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment as ENV } from 'src/environments/environment.development';
import { User } from '../models/user.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private readonly http = inject(HttpClient);

  private readonly baseUrl = ENV.apiBaseUrl;

  getCurrentUser() {
    return this.http.get<User>(`${this.baseUrl}/users/me`);
  }

  getUsers() {
    return this.http.get<User[]>(`${this.baseUrl}/users`);
  }
}
