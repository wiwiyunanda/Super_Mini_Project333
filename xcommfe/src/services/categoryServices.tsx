import axios from 'axios';
import { config } from '../configurations/config';
import { ICategory } from '../interfaces/iCategory';

export const CategoryService = {
    getAll: () => {
        const result = axios.get(config.apiUrl + '/category?pageNum=1&rows=15&orderBy=initial&sort=0')
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
    },
    post: (category : ICategory) => {
        const result = axios.post(config.apiUrl + '/category', category)
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