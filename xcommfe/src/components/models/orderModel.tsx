import { DetailModel } from "./detailModel";

export class OrderModel {
    constructor(){
        this.details =[];
    }
    id: number = 0;
    reference: string = '';
    amount: number = 0;
    details: DetailModel[];
}