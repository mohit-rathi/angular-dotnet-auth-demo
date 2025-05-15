import { Component, inject, OnInit } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';
import { Register } from '../models/register.model';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent implements OnInit {
  private readonly authService = inject(AuthService);
  private readonly router = inject(Router);
  private readonly toastr = inject(ToastrService);

  formGroup!: FormGroup;

  ngOnInit(): void {
    this.prepareFormGroup();
  }

  prepareFormGroup() {
    this.formGroup = new FormGroup({
      role: new FormControl(null, Validators.required),
      email: new FormControl(null, Validators.required),
      password: new FormControl(null, Validators.required),
      confirmPassword: new FormControl(null, [Validators.required, this.confirmPasswordValidator.bind(this)]),
    });
  }

  confirmPasswordValidator(control: AbstractControl): ValidationErrors | null {
    if (!this.formGroup) return null;
    const password = this.formGroup.get('password')?.value;
    const confirmPassword = control.value;
    return password == confirmPassword ? null : { passwordMismatch: true };
  }

  register() {
    const payload = this.formGroup.value as Register;
    this.authService.register(payload).subscribe({
      next: () => {
        this.toastr.success('Registration successful', 'Success');
        this.router.navigate(['/auth', 'login'], { queryParams: { redirect: 'register' } });
      },
      error: (error) => {
        this.toastr.error(error.error.detail, 'Error');
      }
    });
  }
}
