export enum Role {
 ADMIN = 'Admin',
 EMPLOYEE = 'Employee',
}
export interface User {
    id: number;
    name: string;
    birthdate: Date;
    email: string;
    role: number;
}
