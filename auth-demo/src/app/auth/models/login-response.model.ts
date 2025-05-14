import { Role } from "../enums/Role.enum";

export interface LoginResponse {
	id: string;
	email: string;
	role: Role;
	token: string;
}
