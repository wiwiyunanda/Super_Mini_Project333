import { ProductModel } from "./productModel";

 export class DetailModel {
    constructor(){
        this.product = new ProductModel();
    }
    product: ProductModel;
    productId: number = 0;
    price: number = 0;
    quantity: number = 0;
}