import React from 'react';

import {ICategory} from '../../interfaces/iCategory';
import { CategoryService } from '../../services/categoryServices';

interface IProps {
}
interface IState {
    categories: ICategory[]
}

export default class Category extends React.Component<IProps, IState> {
    constructor(props: IProps) {
        super(props);
        this.state = {
            categories: []
        }
    }

    componentDidMount(): void {
        this.loadCategories();
    }
    loadCategories = async () => {
        const result = await CategoryService.getAll();
        if (result.success) {
            this.setState({
                categories: result.result,
            })
            console.log(result);
        } else {
            alert('Error: ' + result.result);
        }
    }

    render() {
        const { categories } = this.state;
        return (           
                <div>
                    <h4> Category </h4>
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
                            categories.map((o: ICategory, idx: number) => {
                                return(                                    
                                    <tr key={o.id}>
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