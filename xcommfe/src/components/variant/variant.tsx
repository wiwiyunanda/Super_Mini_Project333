import React from 'react';

import {IVariant} from '../../interfaces/iVariant';
import { VariantService } from '../../services/variantServices';

interface IProps {
}
interface IState {
    variants: IVariant[]
}

export default class Variant extends React.Component<IProps, IState> {
    constructor(props: IProps) {
        super(props);
        this.state = {
            variants: []
        }
    }

    componentDidMount(): void {
        this.loadVariants();
    }
    loadVariants = async () => {
        const result = await VariantService.getAll();
        if (result.success) {
            this.setState({
                variants: result.result,
            })
            console.log(result);
        } else {
            alert('Error: ' + result.result);
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
                                    <tr key={o.id}>
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