import React from 'react';

import {IVariant} from '../../interfaces/iVariant';

interface IProps {
}
interface IState {
    variants: IVariant[]
}

export default class Variant extends React.Component<IProps, IState> {
    constructor(props: IProps) {
        super(props);
        this.state = {
            variants: [
                {id: 11, categoryId: 1, initial: 'a', name: 'name a', active: true},
                {id: 21, categoryId: 2, initial: 'b', name: 'name b', active: false},
                {id: 31, categoryId: 1, initial: 'c', name: 'name c', active: true}
            ]
        }
    }

    render() {
        const { variants } = this.state;
        return (           
                <div>
                    <h4> Variant </h4>
                    <table>
                        <thead>
                            <tr>
                                <th>No.</th>
                                <th>categoryId</th>
                                <th>Initial</th>
                                <th>Name</th>
                                <th>Active</th>
                            </tr>
                        </thead>
                        <tbody>
                            {
                            variants.map((o: IVariant, idx: number) => {
                                return(                                    
                                    <tr>
                                        <td>{idx + 1}</td>
                                        <td>{o.categoryId}</td>
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