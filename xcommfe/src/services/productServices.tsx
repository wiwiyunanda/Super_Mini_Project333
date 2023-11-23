import axios from 'axios';
import { config } from '../configurations/config';
import { IProduct } from '../interfaces/iProduct';

export const ProductService = {
    getAll: () => {
        const result = axios.get(config.apiUrl + '/Products?pageNum=1&rows=10&orderBy=initial&sort=0')
        // https://localhost:7016/api/Products?pageNum=1&rows=5&orderBy=initial&sort=0
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
        const result = axios.get(config.apiUrl + '/Products/' + id)
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

    post: (product : IProduct) => {
        const result = axios.post(config.apiUrl + '/Products/', product)
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

    update: (id:number, product : IProduct) => {
        const result = axios.put(config.apiUrl + '/Products/' + id, product)
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
        const result = axios.put(config.apiUrl + `/Products/changestatus/${id}/${status}`)
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