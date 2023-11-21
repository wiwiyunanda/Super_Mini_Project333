import axios from 'axios';
import { config } from '../configurations/config';

export const CategoryService = {
    getAll: () => {
        const result = axios.get(config.apiUrl + '/category?pageNum=1&rows=10&orderBy=initial&sort=0')
        .then(respons => {
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
    }
}