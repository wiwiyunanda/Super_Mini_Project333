import React from "react";
import { FaPlus } from "react-icons/fa6";

import { IProduct } from "../../interfaces/iProduct";
import { IVariant } from "../../interfaces/iVariant";
import { ICategory } from "../../interfaces/iCategory";
import { IPagination } from "../../interfaces/iPagination";
import { config } from "../../configurations/config";
import { ProductService } from "../../services/productServices";
import { CategoryService } from "../../services/categoryServices";

interface IProps {}
interface IState {
  product: IProduct;
  products: IProduct[];
  pagination: IPagination;
  categories: ICategory[];
  noImage: any;
}
export default class Home extends React.Component<IProps, IState> {
  newPagination: IPagination = {
    pageNum: 1,
    rows: config.rowsPerPage[0],
    search: "",
    orderBy: "id",
    sort: 1,
    pages: 0,
  };
  newCategory: ICategory = {
    id: 0,
    initial: "",
    name: "",
    active: false,
  };
  newVariant: IVariant = {
    id: 0,
    categoryId: 0,
    initial: "",
    name: "",
    active: false,
    category: this.newCategory,
  };
  newProduct: IProduct = {
    id: 0,
    categoryId: 0,
    variantId: 0,
    galleryId: 0,
    initial: "",
    name: "",
    active: false,
    variant: this.newVariant,
    base64: "",
    description: "",
    price: 0,
    stock: 0,
  };
  constructor(props: IProps) {
    super(props);
    this.state = {
      product: this.newProduct,
      products: [],
      categories: [],
      pagination: this.newPagination,
      noImage: config.noImage,
    };
  }
  componentDidMount(): void {
    this.loadProducts();
  }

  loadProducts = async () => {
    const { pagination } = this.state;
    await ProductService.getPagination(pagination).then((result) => {
      console.log(result);
      if (result.success) {
        CategoryService.getAll(pagination).then((catResult) => {
          console.log(catResult);
          if (catResult.success) {
            this.setState({
              categories: catResult.result,
            });
          }
          console.log(result.result);
          this.setState({
            products: result.result,
          });
        });
      } else {
        alert("Error: " + result.result);
      }
    });
  };

  render() {
    const { products, noImage } = this.state;
    return (
      <div>
        <div className="text-left text-3xl pt-5 text-center dark:text-purple-700">
            Welcome to XCommerce--IKA
        </div>
        <div className="flex max-w-full w-full p-2 justify-center h-screen">
        <div className="grid grid-cols-4 md:grid-cols-4 gap-4 items-center">
          {products.map((o) => {
            return (
              <div key={o.id}>
                <img
                  className="h-auto rounded-lg mx-auto"
                  src={o.base64 ? o.base64 : noImage}
                  alt="Kosong"
                  width={64}
                  height={64}
                />
                <div className="flex-col p-5 text-center">
                  <h5 className="mb-2 text-lg font tracking-tight text-gray-900 ">
                    {o.name}
                  </h5>
                  <p className="mb-3 font-normal text-gray-600 dark:text-purple-500">
                    {"Rp. " + o.price}
                  </p>
                  <a>Add Item</a>
                </div>
              </div>
            );
          })}
        </div>
      </div>
      </div>
    );
  }
}
