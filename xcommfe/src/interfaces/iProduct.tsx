import { ICategory } from "./iCategory";
import { IVariant } from "./iVariant";

export interface IProduct extends ICategory{
    variantId: number;
    variant: IVariant;
    description: string;
    price: number;
    stock: number;
}
