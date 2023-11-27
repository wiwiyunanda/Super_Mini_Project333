import axios from 'axios';
import { config } from '../configurations/config';
import { IVariant } from '../interfaces/iVariant';

export const VariantService = {
    getAll: () => {
        const result = axios.get(config.apiUrl + '/Variant?pageNum=1&rows=10&orderBy=initial&sort=0')
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
        const result = axios.get(config.apiUrl + '/Variant/' + id)
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

    getByParentId: (id: number) => {
        const result = axios.get(config.apiUrl + '/Variant/category/' + id)
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

    post: (variant : IVariant) => {
        const {category, ...newVariant } =variant;
        console.log(variant, newVariant);
        const result = axios.post(config.apiUrl + '/Variant/', newVariant)
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

    update: (id:number, variant : IVariant) => {
        const result = axios.put(config.apiUrl + '/Variant/' + id, variant)
        .then(respons => {
          // console.log(respons);
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
        const result = axios.put(config.apiUrl + `/variant/changestatus/${id}/${status}`)
        .then(respons => {
          // console.log(respons);
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