import axios from "axios";
import { config } from '../configurations/config';
import { IAuthentication } from "../interfaces/iAuthentication";
import { AccountModel } from "../components/models/accountModel";

export const AuthService = {
    login: (auth: IAuthentication) => {
        var result = axios.post(config.apiUrl + '/Account/Login', auth)
            .then(respons => {
                const account: AccountModel = respons.data;
                // alert(account);
                localStorage.setItem('userName', account.userName);
                localStorage.setItem('firstName', account.firstName);
                localStorage.setItem('lastName', account.lastName);
                localStorage.setItem('token', account.token);
                localStorage.setItem('roles', JSON.stringify(account.roles));
                console.log(JSON.stringify(account.roles));
                return {
                    success: (respons.status == 200),
                    result: {
                        userName: account.userName,
                        firstName: account.firstName,
                        lastName: account.lastName
                    }
                }
            }).catch(error => {
                return {
                    success: false,
                    result: error
                }
            });
        return result;
    },
    getToken: () => {
        return localStorage.getItem('token');
    },
    getAccount: (): AccountModel => {
        let rolStr: any = localStorage.getItem("roles");
        let roles: string[] = JSON.parse(rolStr); 
        console.log(roles);
        return {
            id: 0,
            userName: localStorage.getItem('userName') || '',
            firstName: localStorage.getItem('firstName') || '',
            lastName: localStorage.getItem('lastName') || '',
            active: true,
            email: '',
            token: '',
            roles:  roles || []
        }
    },
    logout: () => {
        localStorage.removeItem('userName');
        localStorage.removeItem('firstName');
        localStorage.removeItem('lastName');
        localStorage.removeItem('token');
        localStorage.removeItem('roles');
    }
}
