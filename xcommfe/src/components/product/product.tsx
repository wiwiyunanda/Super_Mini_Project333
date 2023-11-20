import React from 'react';

import {IProduct} from '../../interfaces/iProduct';

interface IProps {
}
interface IState {
    products: IProduct[]
}

export default class Product extends React.Component<IProps, IState> {
    constructor(props: IProps) {
        super(props);
        this.state = {
            products: [
                {id: 1, initial: 'a', name: 'name a', active: true},
                {id: 2, initial: 'b', name: 'name b', active: false},
                {id: 3, initial: 'c', name: 'name c', active: true}
            ]
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
                                <th>Initial</th>
                                <th>Name</th>
                                <th>Active</th>
                            </tr>
                        </thead>
                        <tbody>
                            {
                            products.map((o: IProduct, idx: number) => {
                                return(                                    
                                    <tr>
                                        <td>{idx + 1}</td>
                                        <td>{o.initial}</td>
                                        <td>{o.name}</td>
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