import { ICategory } from "./iCategory";
import { IVariant } from "./iVariant";

export interface IProduct extends ICategory {
  categoryId: number;
  variantId: number;
  variant: IVariant;
  galleryId: number;
  base64: string;
  description: string;
  price: number;
  stock: number;
}
