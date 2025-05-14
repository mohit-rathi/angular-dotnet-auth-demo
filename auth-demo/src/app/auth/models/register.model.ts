import { Role } from "../enums/Role.enum";

export interface Register {
	email: string;
	password: string;
	role: Role;
}
