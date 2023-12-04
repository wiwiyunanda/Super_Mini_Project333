import React from "react";
import { buttonCls, inputCls } from "../style/styledComponent";
import { IProduct } from "../../interfaces/iProduct";
import ProductList from "./productList";
import { DetailModel } from "../models/detailModel";
import { ProductService } from "../../services/productServices";
import { ProductModel } from "../models/productModel";
import { OrderService } from "../../services/orderService";

interface IProps {}

interface IState {
  products: IProduct[];
  details: DetailModel[];
  showModal: boolean;
}

export default class Order extends React.Component<IProps, IState> {
  constructor(props: IProps) {
    super(props);
    this.state = {
      products: [],
      details: [],
      showModal: false,
    };
  }

  newItem = () => {
    this.setState({
      showModal: true,
    });
  };

  setShowModal = (val: boolean) => {
    this.setState({
      showModal: val
    })
  }

  selectProduct = (id: number) => {
    ProductService.getById(id).then((result) => {
      if (result.success) {
        const product: ProductModel = result.result;

        const item: DetailModel = {
          product: {
            id: product.id,
            base64: product.base64,
            initial: product.initial,
            name: product.name,
            description: product.description,
            price: product.price,
            stock: product.stock,
          },
          productId: product.id,
          price: product.price,
          quantity: 1
        }

        const { details } = this.state;
        details.push(item);
        this.setState({
          details: details,
          showModal: false
        })
      }
    })
  }

  removeProduct = (idx: number) => {
    const { details } = this.state;
    details.splice(idx, 1);
    this.setState({
      details: details,
    })
  }

  changeHandler = (idx: number) => (event: any) => {
    const { details } = this.state;
    details[idx].quantity = event.target.value;
    this.setState({
      details: details,
    })
  }

  submitHandler = () => {
    OrderService.post(this.state.details)
    .then(result =>{
      if (result.success){
        this.setState({
          details: []
        })
      }
    }).catch(error => {
      alert(error)
    })
  }

  render() {
    const { showModal, details } = this.state;
    return (
      <div>
        {JSON.stringify({amount: details.reduce((a, b) => (a+ (b['price'] * b ['quantity']) || 0), 0), details})}
        <div className="text-left text-3xl pt-5">Orders</div>
        <div className="flex" aria-label="Button">
          <button className={buttonCls} onClick={this.newItem}>New Item</button>
          <button className={buttonCls} onClick={this.submitHandler}>Payment</button>
        </div>
        <table className="w-full text-sm ">
          <thead className="text-xs text-white uppercase bg-gray-700">
            <tr className="border-b bg-gray-900 border-gray-700 text-center">
              <th scope="col" className="px-6 py-3 w-14 h-14">
                Image
              </th>
              <th scope="col" className="px-6 py-3 w-14 h-14">
                Product
              </th>
              <th scope="col" className="px-6 py-3 w-14 h-14">
                Price
              </th>
              <th scope="col" className="px-6 py-3 w-14 h-14">
                Quantity
              </th>
              <th scope="col" className="px-6 py-3 w-14 h-14">
                Total
              </th>
              <th scope="col" className="px-6 py-3 w-14 h-14">
                Remove
              </th>
            </tr>
          </thead>
          <tbody>
            {details.map((o: DetailModel, idx) => {
              return (
                <tr
                  key={idx}
                  className="border-b bg-gray-900 border-gray-700 text-white text-center"
                >
                  <td className="px-6 py-4">
                    <img src={o.product.base64} width={128} height={128}></img>
                  </td>
                  
                  <td className="px-6 py-4">
                    <input readOnly = {true} type="text" className={inputCls} value={o.product.name}/>
                  </td>
                  <td className="px-6 py-4">
                    <input readOnly = {true} type="number" className={inputCls} value={o.price}/>
                  </td>
                  <td className="px-6 py-4">
                    <input type="number" className={inputCls} value={o.quantity} onChange={this.changeHandler(idx)}/>
                  </td>
                  <td className="px-6 py-4">
                    <input readOnly = {true} type="number" className={inputCls} value={o.price* o.quantity}/>
                  </td>
                  <td className="px-6 py-4">
                    <button className={buttonCls} onClick={() => this.removeProduct(idx)}>Remove</button>
                  </td>
                </tr>
              );
            })}
          </tbody>
          <tfoot className="text-xs text-white uppercase bg-gray-700">
          <th colSpan={3} scope="col" className="px-6 py-3 w-14 h-14">
                Grand Total
          </th>
          <th scope="col" className="px-6 py-3 w-14 h-14">
                <input readOnly={true} type="number" className={inputCls} value={details.reduce((a, b) => (a+b['quantity'] || 0), 0)} />
          </th>
          <th scope="col" className="px-6 py-3 w-14 h-14">
                <input readOnly={true} type= "number" className={inputCls} value={details.reduce((a, b) => (a+ b['price'] * b['quantity'] || 0), 0)} />
          </th>
          <th scope="col" className="px-6 py-3 w-14 h-14">
                Remove
          </th>
          </tfoot>
        </table>
        {showModal ? (
          <div className="flex justify-center items-center overflow-x-hidden overflow-y-auto fixed inset-0 z-50 outline-none focus:outline-none ">
            <div className="relative w-auto my-6 mx-auto max-w-3xl ">
              <div className="border-0 rounded-lg shadow-lg relative flex flex-col w-full bg-gray outline-none focus:outline-none bg-gray-900">
                <div className="flex items-start justify-between p-5 border-b border-solid border-gray-300 rounded-t ">
                  <h3 className="text-3xl text-gray-900 text-gray">
                    Product List
                  </h3>
                  <button
                    className="bg-transparent border-0 text-black float-right"
                    onClick={() => this.setShowModal(false)}
                  >
                    <span className="text-black opacity-7 h-6 w-6 text-xl block bg-gray-400 py-0 rounded-full">
                      x
                    </span>
                  </button>
                </div>
                <div className="relative p-6 flex-auto">
                  <ProductList selectProduct={this.selectProduct} />
                </div>
              </div>
            </div>
          </div>
        ) : null}
      </div>
    );
  }
}
