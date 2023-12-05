import axios from 'axios';
import { config } from '../configurations/config';
import { IPagination } from '../interfaces/iPagination';

export const CartService = {
    getAll: (pg: IPagination) => {
        const searchStr = pg.search.length > 0 ? `&search=${pg.search}`: ``;
        const result = axios.get(config.apiUrl + `/Cart?pageNum=${pg.pageNum}&rows=${pg.rows}${searchStr}&orderBy=${pg.orderBy}&sort=${pg.sort}`, {headers: config.headers()})
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
}