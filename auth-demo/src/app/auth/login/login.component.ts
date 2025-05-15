import { Component, inject, OnInit, signal } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../services/auth.service';
import { Login } from '../models/login.model';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  private readonly authService = inject(AuthService);
  private readonly router = inject(Router);
  private readonly route = inject(ActivatedRoute);
  private readonly toastr = inject(ToastrService);

  formGroup!: FormGroup;
  isRegisterRedirect = signal<boolean>(false);
  isUnautheniticatedRedirect = signal<boolean>(false);

  ngOnInit(): void {
    this.prepareFormGroup();
    this.route.queryParams.subscribe({
      next: (params) => {
        this.isRegisterRedirect.set(false);
        this.isUnautheniticatedRedirect.set(false);
        if (params['redirect'] == 'register') {
          this.isRegisterRedirect.set(true);
        }
        if (params['redirect'] == 'unauthenticated') {
          this.isUnautheniticatedRedirect.set(true);
        }
      }
    });
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
        this.toastr.success('Login successful', 'Success');
        this.router.navigate(['/dashboard']);
      },
      error: (error) => {
        this.toastr.error(error.error.detail, 'Error');
      }
    });
  }
}
