import axios from 'axios';
import { config } from '../configurations/config';
import { ICategory } from '../interfaces/iCategory';

export const CategoryService = {
    getAll: () => {
        const result = axios.get(config.apiUrl + '/category?pageNum=1&rows=15&orderBy=initial&sort=0')
        .then(respons => {
            console.log(respons);
            return {
                success : respons.data.success,
                result :respons.data.data
            }
        })
        .catch(error => {
            return{
                success : false,
                result : error
            }
        });
        return result;
    },

    getById: (id: number) => {
        const result = axios.get(config.apiUrl + '/category/' + id)
        .then(respons => {
            return {
                success : respons.status == 200,
                result :respons.data,
            };
        })
        .catch((error) => {
            return{
                success : false,
                result : error,
            };
        });
        return result;
    },

    post: (category : ICategory) => {
        const result = axios.post(config.apiUrl + '/category', category)
        .then(respons => {
           // console.log(respons);
            return {
                success : (respons.status == 200),
                result :respons.data.data
            }
        })
        .catch(error => {
            return{
                success : false,
                result : error
            }
        });
        return result;
    },

    update: (id:number, category : ICategory) => {
        const result = axios.put(config.apiUrl + '/category/' + id, category)
        .then(respons => {
           console.log(respons);
            return {
                success : (respons.status == 200),
                result :respons.data
            }
        })
        .catch(error => {
            return{
                success : false,
                result : error
            }
        });
        return result;
    },

    changeStatus: (id:number, status : boolean) => {
        const result = axios.put(config.apiUrl + '/category/changestatus/' + id, status)
        .then(respons => {
           console.log(respons);
            return {
                success : (respons.status == 200),
                result :respons.data
            }
        })
        .catch(error => {
            return{
                success : false,
                result : error
            }
        });
        return result;
    }
}