import React from "react";
import { IPagination } from "../../interfaces/iPagination";
import { ProductModel } from "../models/productModel";
import { ProductService } from "../../services/productServices";

interface IProps {
  selectProduct: any;
}

interface IState {
  products: ProductModel[];
  pagination: IPagination;
}

export default class ProductList extends React.Component<IProps, IState> {
  newPagination: IPagination = {
    pageNum: 1,
    rows: 50,
    search: "",
    orderBy: "id",
    sort: 0,
    pages: 0,
  };

  constructor(props: IProps) {
    super(props);
    this.state = {
      pagination: this.newPagination,
      products: [],
    };
  }

  componentDidMount(): void {
    this.loadProducts();
  }

  loadProducts = () => {
    ProductService.getPagination(this.state.pagination)
      .then((result) => {
        if (result.success) {
          this.setState({
            products: result.result,
          });
        }
      })
      .catch((error) => {
        alert(error);
      });
  };

  render() {
    const { products } = this.state;
    const { selectProduct } = this.props;
    return (
      <table>
        <thead>
          <tr>
            <th>Search</th>
            <th colSpan={2}>
              <input type="text" />
            </th>
            <th>
              <button></button>
            </th>
          </tr>
          <tr className="text-white">
            <th></th>
            <th>Initial/Name</th>
            <th>Price</th>
            <th>Stock</th>
          </tr>
        </thead>
        <tbody>
          {products.map((o) => {
            return (
              <tr onClick={() => selectProduct(o.id)} className="text-white">
                <td>
                  <img src={o.base64} height={64} width={64} />
                </td>
                <td>{o.initial}</td>
                <td>{o.price}</td>
                <td>{o.stock}</td>
              </tr>
            );
          })}
        </tbody>
      </table>
    );
  }
}
