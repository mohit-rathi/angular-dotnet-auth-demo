import { CanActivateFn, Router } from '@angular/router';
import { User } from 'src/app/models/user.model';
import { Role } from '../enums/Role.enum';
import { inject } from '@angular/core';

export const adminGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);

  const user = JSON.parse(localStorage.getItem('user')!) as User;
  if (user.role !== Role.Admin) return router.parseUrl('/unauthorized');
  return true;
};
