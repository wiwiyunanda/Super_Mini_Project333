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
                localStorage.setItem('laststName', account.lastName);
                localStorage.setItem('token', account.token);
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
    logout: () => {
        localStorage.removeItem('userName');
        localStorage.removeItem('firstName');
        localStorage.removeItem('lastName');
        localStorage.removeItem('token');
    }
}
