import { inject } from '@angular/core';
import { CanActivateFn, CanMatchFn, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { catchError, of } from 'rxjs';

export const authGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  const router = inject(Router);
  const token = localStorage.getItem('token');
  if (!token) return router.createUrlTree(['/auth', 'login'], { queryParams: { redirect: 'unauthenticated' } });
  return authService.verify().pipe(catchError(() => of(router.createUrlTree(['/auth', 'login'], { queryParams: { redirect: 'unauthenticated' } }))));
};
