import React from "react";
import { FaPlus, FaCartShopping } from "react-icons/fa6";

import { ProductModel } from '../models/productModel';
import { IPagination } from '../../interfaces/iPagination';
import { ProductService } from '../../services/productServices';
import { CartModel } from '../models/cartModel';
import { CartService } from '../../services/cartService';


interface IProps {}
interface IState {
  carts: CartModel[];
  pagination: IPagination;
}
export default class Home extends React.Component<IProps, IState> {
  newPagination: IPagination = {
    pageNum: 1,
    rows: 25,
    search: "",
    orderBy: "id",
    sort: 1,
    pages: 0,
  };
  // newCategory: ICategory = {
  //   id: 0,
  //   initial: "",
  //   name: "",
  //   active: false,
  // };
  // newVariant: IVariant = {
  //   id: 0,
  //   categoryId: 0,
  //   initial: "",
  //   name: "",
  //   active: false,
  //   category: this.newCategory,
  // };
  // newProduct: IProduct = {
  //   id: 0,
  //   categoryId: 0,
  //   variantId: 0,
  //   galleryId: 0,
  //   initial: "",
  //   name: "",
  //   active: false,
  //   variant: this.newVariant,
  //   base64: "",
  //   description: "",
  //   price: 0,
  //   stock: 0,
  // };
  constructor(props: IProps) {
    super(props);
    this.state = {
      carts:[],
      pagination: this.newPagination,
    };
  }
  componentDidMount(): void {
    console.log('componentDidMount');
    this.loadCarts();
  }

  loadCarts = () => {
    CartService.getAll(this.state.pagination)
    .then(result => {
      if (result.success){
        this.setState({
          carts: result.result
        })
      }
    })
  }

  render() {
    const { carts } = this.state;
    return (
      <div className="flex flex-col">
        <div className="flex text-3xl pt-5 text-center rounded-lg bg-purple-700  p-4 m-2 text-white w-fit justify-center items-center mx-auto gap-2">
          Welcome to XCommerce-IKA
          <FaCartShopping />
        </div>
        <div className="flex max-w-full w-full p-2 justify-center h-screen">
          <div className="grid grid-cols-4 md:grid-cols-4 gap-1 items-center">
            {carts.map((o: CartModel) => {
              return (
                <div key={o.id}>
                  <img
                    className="h-auto rounded-lg mx-auto"
                    src={o.base64}
                    width={64}
                    height={64}
                  />
                  <div className="flex-col p-1 text-center">
                    <h5 className="mb-1 text-lg font tracking-tight text-gray-900 ">
                      {o.name}
                    </h5>
                    <p className="mb-1 font-normal text-gray-600 dark:text-purple-500">
                      {"Rp. " + o.price}
                    </p>
                    <p className="mb-1 font-normal text-gray-600 dark:text-purple-500">
                      {o.quantity}
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
