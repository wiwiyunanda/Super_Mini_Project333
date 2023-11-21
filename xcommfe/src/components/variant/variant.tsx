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
            <div className="text-left text-3xl pt-5 text-center">Variants</div>
            <div className="flex" aria-label="Button">
                <button className="my-8 justify-start h-8 px-4 text-green-100 transition-colors duration-150 
                 bg-green-700 rounded focus:shadow-outline hover:bg-green-800">Create New </button>
            </div>            
            <table className="w-full text-sm text-left text-gray-500 text-gray-400">
                <thead className="text-xs text-gray-700 uppercase bg-gray-50 bg-gray-700 text-gray-400">
                    <tr className="border-b bg-gray-900 border-gray-700">
                        <th scope="col" className="px-6 py-3 w-14 h-14">
                            {/* grow flex-none  */}
                            Id
                            </th>
                        <th scope="col" className="px-6 py-3 w-14 h-14">
                            CategoryId
                        </th>
                        <th scope="col" className="px-6 py-3 w-14 h-14">
                            Initial
                        </th>
                        <th scope="col" className="px-6 py-3 w-14 h-14">
                            Name
                        </th>
                        <th scope="col" className="px-6 py-3 w-14 h-14">
                            Active
                        </th>
                        <th scope="col" className="px-6 py-3 w-14 h-14">
                            Action
                        </th>
                    </tr>
                </thead>
                <tbody>
                    {
                        variants?.map((cat: any) => {
                            return <tr className="border-b bg-gray-800 border-gray-700">
                                <td scope="row" className="px-6 py-4 font-medium text-white-900 whitespace-nowrap text-white">
                                    {cat.id}
                                </td>
                                <td className="px-6 py-4">
                                    {cat.CategoryId}
                                </td>
                                <td className="px-6 py-4">
                                    {cat.initial}
                                </td>
                                <td className="px-6 py-4">
                                    {cat.name}
                                </td>
                                <td className="px-6 py-4">
                                    <div className="flex items-center">
                                        <input checked id="checked-checkbox" type="checkbox" value={cat.active} className="w-4 h-4 text-blue-600 bg-gray-100 border-gray-300 rounded focus:ring-blue-500 dark:focus:ring-blue-600 dark:ring-offset-gray-800 focus:ring-2 dark:bg-gray-700 dark:border-gray-600" />
                                    </div>
                                </td>
                                <td className="px-6 py-4">
                                    <div className="inline-flex" role="group" aria-label="Button group">
                                        <button className="h-10 px-5 text-green-100 transition-colors duration-150 bg-green-700 rounded-l-lg focus:shadow-outline hover:bg-green-800">Edit</button>
                                        <button className="h-10 px-5 text-blue-100 transition-colors duration-150 bg-blue-700 rounded-r-lg focus:shadow-outline hover:bg-blue-800">Status</button>
                                    </div>
                                </td>
                            </tr>
                        })
                    }
                </tbody>
            </table>
        </div>
        )
    }
}