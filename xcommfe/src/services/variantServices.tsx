import axios from 'axios';
import { config } from '../configurations/config';
import { IVariant } from '../interfaces/iVariant';

export const VariantService = {
    getAll: () => {
        const result = axios.get(config.apiUrl + '/Variants?pageNum=1&rows=10&orderBy=initial&sort=0')
        // https://localhost:7016/api/Variants?pageNum=1&rows=5&orderBy=initial&sort=0
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
    post: (variant : IVariant) => {
        const result = axios.post(config.apiUrl + '/Variants', variant)
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