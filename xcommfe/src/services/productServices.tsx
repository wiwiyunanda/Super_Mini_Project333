import axios from "axios";
import { config } from "../configurations/config";
import { IProduct } from "../interfaces/iProduct";
import { IPagination } from "../interfaces/iPagination";

export const ProductService = {
  getAll: () => {
    const result = axios
      .get(config.apiUrl + '/Products/' , {headers: config.headers()})
      .then((respons) => {
        // console.log(respons);
        return {
          success: respons.status === 200,
          result: respons.data,
        };
      })
      .catch((error) => {
        return {
          success: false,
          result: error,
        };
      });
    return result;
  },

  post: (products: IProduct) => {
    const { variant, ...newProduct } = products;
    const result = axios
      .post(config.apiUrl + '/Products/', newProduct , {headers: config.headers()})
      .then((respons) => {
        // console.log(respons);
        return {
          success: respons.status === 200,
          result: respons.data,
        };
      })
      .catch((error) => {
        return {
          success: false,
          result: error,
        };
      });
    return result;
  },

  update: (id: number, products: IProduct) => {
    const result = axios
      .put(config.apiUrl + '/Products/' + id, products , {headers: config.headers()})
      .then((respons) => {
        // console.log(respons);
        return {
          success: respons.status === 200,
          result: respons.data,
        };
      })
      .catch((error) => {
        return {
          success: false,
          result: error,
        };
      });
    return result;
  },

  changeStatus: (id: number, status: boolean) => {
    const result = axios
      .put(config.apiUrl + `/Products/changestatus/${id}?status=${status}` , null, {headers: config.headers()})
      .then((respons) => {
        // console.log(respons);
        return {
          success: respons.status === 200,
          result: respons.data,
        };
      })
      .catch((error) => {
        return {
          success: false,
          result: error,
        };
      });
    return result;
  },

  getById: (id: number) => {
    const result = axios
      .get(config.apiUrl + '/Products/' + id , {headers: config.headers()})
      .then((respons) => {
        return {
          success: respons.status === 200,
          result: respons.data,
        };
      })
      .catch((error) => {
        return {
          success: false,
          result: error,
        };
      });
    return result;
  },

  getPagination: (pg: IPagination) => {
    const searchStr = pg.search.length > 0 ? `&search=${pg.search}` : ``;
    const result = axios
      .get(
        config.apiUrl +
          `/Products?pageNum=${pg.pageNum}&rows=${pg.rows}${searchStr}&orderBy=${pg.orderBy}&sort=${pg.sort}` , {headers: config.headers()}
      )
      .then((respons) => {
        console.log(respons);
        return {
          success: respons.data.success,
          result: respons.data.data,
          pages: respons.data.pages,
        };
      })
      .catch((error) => {
        return {
          success: false,
          result: error,
          pages: 0,
        };
      });
    return result;
  },

  changeGallery: (id: number, galleryId: number) => {
    const result = axios
      .put(config.apiUrl + `/Products/changeGallery/${id}/${galleryId}` , {headers: config.headers()})
      .then((respons) => {
        console.log(respons);
        return {
          success: respons.status == 200,
          result: respons.data,
        };
      })
      .catch((error) => {
        // console.log(error);
        return {
          success: false,
          result: error,
        };
      });
    return result;
  },
};
