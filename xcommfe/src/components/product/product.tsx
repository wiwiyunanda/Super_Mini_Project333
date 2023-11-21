import React from 'react';

import {IProduct} from '../../interfaces/iProduct';
import { ProductService } from '../../services/productServices';

interface IProps {
}
interface IState {
    products: IProduct[]
}

export default class Product extends React.Component<IProps, IState> {
    constructor(props: IProps) {
        super(props);
        this.state = {
            products: []
        }
    }

    componentDidMount(): void {
        this.loadProducts();
    }
    loadProducts = async () => {
        const result = await ProductService.getAll();
        if (result.success) {
            this.setState({
                products: result.result,
            })
            console.log(result);
        } else {
            alert('Error: ' + result.result);
        }
    }

    render() {
        const { products } = this.state;
        return (           
                <div>
                    <h4> Product </h4>
                    <table>
                        <thead>
                            <tr>
                                <th>No.</th>
                                <th>VariantId</th>
                                <th>Initial</th>
                                <th>Name</th>
                                <th>Description</th>
                                <th>Price</th>
                                <th>Stock</th>
                                <th>Active</th>

                            </tr>
                        </thead>
                        <tbody>
                            {
                            products.map((o: IProduct, idx: number) => {
                                return(                                    
                                    <tr key={o.id}>
                                        <td>{idx + 1}</td>
                                        <td>{o.variantId}</td>
                                        <td>{o.initial}</td>
                                        <td>{o.name}</td>
                                        <td>{o.description}</td>
                                        <td>{o.price}</td>
                                        <td>{o.stock}</td>
                                        <td>{o.active? 'True' : 'False'}</td>
                                    </tr>
                                )
                            })
                        }                           
                        </tbody>
                    </table>
                </div>                
        )
    }
}