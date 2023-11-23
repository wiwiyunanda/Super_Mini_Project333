import { ICategory } from "./iCategory";

export interface IVariant extends ICategory{
    categoryId: number;
    category: ICategory
}
