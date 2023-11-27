import axios from 'axios';
import { config } from '../configurations/config';
import { IGallery } from '../interfaces/iGallery';
import { IPagination } from '../interfaces/iPagination';

export const GalleryService = {
    getAll: (pg: IPagination) => {
        const searchStr = pg.search.length > 0 ? `&search=${pg.search}` : ``;
        const result = axios.get(config.apiUrl + `/Gallery?pageNum=${pg.pageNum}&rows=${pg.rows}${searchStr}&orderBy=${pg.orderBy}&sort=${pg.sort}`)
            .then(respons => {
                console.log(respons);
                return {
                    success: respons.data.success,
                    result: respons.data.data,
                    pages: respons.data.pages
                }
            })
            .catch(error => {
                return {
                    success: false,
                    result: error,
                    pages: 0
                }
            });
        return result;
    },
    post: (gallery: IGallery) => {
        console.log(gallery);
        const result = axios.post(config.apiUrl + '/Gallery', gallery)
            .then(respons => {
                console.log(respons);
                return {
                    success: (respons.status == 200),
                    result: respons.data
                }
            })
            .catch(error => {
                // console.log(error);
                return {
                    success: false,
                    result: error
                }
            });
        return result;
    }
}
