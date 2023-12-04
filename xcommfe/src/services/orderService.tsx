import axios from 'axios';
import { config } from '../configurations/config';
import { OrderModel } from '../components/models/orderModel';
import { DetailModel } from '../components/models/detailModel';

export const OrderService = {
    post: (details: DetailModel[]) => {
        let newDetails: any = [];
        let amount: number = 0;

        details.map(o => {
            const { product, ...exProduct } = o;
            newDetails.push(exProduct);
            amount += o.price * o.quantity;
        })

        let order = { id: 0, reference: '', amount: amount, details: newDetails };

        console.log(order);

        const result = axios.post(config.apiUrl + '/order', order)
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
    },
}
