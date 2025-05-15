import { Role } from '../auth/enums/Role.enum';

export interface User {
  id: string;
  email: string;
  role: Role;
}
