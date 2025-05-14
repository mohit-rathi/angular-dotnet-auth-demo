import { Component, inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../services/auth.service';
import { Login } from '../models/login.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  private readonly authService = inject(AuthService);
  private readonly router = inject(Router);

  formGroup!: FormGroup;

  ngOnInit(): void {
    this.prepareFormGroup();
  }

  prepareFormGroup() {
    this.formGroup = new FormGroup({
      email: new FormControl(null, Validators.required),
      password: new FormControl(null, Validators.required),
    });
  }

  login() {
    const payload = this.formGroup.value as Login;
    this.authService.login(payload).subscribe({
      next: (response) => {
        localStorage.setItem('token', response.token);
        localStorage.setItem('user', JSON.stringify({ email: response.email, role: response.role }));
        this.router.navigate([]);
      },
      error: (error) => {
        console.error(error);
      }
    });
  }
}
