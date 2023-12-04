export class AccountModel {
    id: number = 0;
    userName: string = '';
    firstName: string = '';
    lastName: string = '';
    email: string = '';
    active: boolean = true;
    token: string = '';
    roles: string[] = [];
}
