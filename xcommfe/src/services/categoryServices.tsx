import axios from 'axios';
import { config } from '../configurations/config';
import { ICategory } from '../interfaces/iCategory';
import { IPagination } from '../interfaces/iPagination';

export const CategoryService = {
    getAll: (pg: IPagination) => {
        const searchStr = pg.search.length > 0 ? `&search=${pg.search}`: ``;
        const result = axios.get(config.apiUrl + `/category?pageNum=${pg.pageNum}&rows=${pg.rows}${searchStr}&orderBy=${pg.orderBy}&sort=${pg.sort}`, {headers: config.headers()})
        .then(respons => {
            console.log(respons);
            return {
                success : respons.data.success,
                result :respons.data.data,
                pages: respons.data.pages
            }
        })
        .catch(error => {
            return{
                success : false,
                result : error,
                pages: 0
            }
        });
        return result;
    },

    getById: (id: number) => {
        const result = axios.get(config.apiUrl + '/category/' + id , {headers: config.headers()})
        .then(respons => {
            return {
                success : respons.status == 200,
                result :respons.data
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
        const result = axios.post(config.apiUrl + '/category/', category , {headers: config.headers()})
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
        const result = axios.put(config.apiUrl + '/category/' + id, category , {headers: config.headers()})
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
        const result = axios.put(config.apiUrl + `/category/changestatus/${id}/${status}` ,null, {headers: config.headers()})
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