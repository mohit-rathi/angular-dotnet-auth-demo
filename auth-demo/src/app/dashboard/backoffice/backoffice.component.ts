import { Component, inject, OnInit, signal } from '@angular/core';
import { Role } from 'src/app/auth/enums/Role.enum';
import { User } from 'src/app/models/user.model';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-backoffice',
  templateUrl: './backoffice.component.html',
  styleUrls: ['./backoffice.component.css'],
})
export class BackofficeComponent implements OnInit {
  private readonly userService = inject(UserService);

  users = signal<User[]>([]);

  ngOnInit(): void {
    this.userService.getUsers().subscribe({
      next: (users) => {
        this.users.set(users);
      },
    });
  }

  getRoleName(roleId: Role) {
    switch (roleId) {
      case Role.Admin:
        return 'Admin';
      case Role.User:
        return 'User';
      default:
        return 'Unknown';
    }
  }
}
