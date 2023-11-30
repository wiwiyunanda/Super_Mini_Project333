import React from 'react';

import { ECommand } from '../../enums/eCommand';
import { ICategory } from '../../interfaces/iCategory';
import { IVariant } from '../../interfaces/iVariant';
import { IProduct } from '../../interfaces/iProduct';

interface IProps {

    product: IProduct;
    categories: ICategory[];
    variants: IVariant[];
    command: ECommand;
    changeHandler: any;
    checkBoxHandler: any;
}
interface IState {
}

export default class Form extends React.Component<IProps, IState> {
    constructor(props: IProps) {
        super(props);
    }

    render() {
        const {categories, variants, product, command, changeHandler, checkBoxHandler } = this.props;
        return (           
            <>
                <form>
                <div className="mb-6">
                        <label className="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Select Category</label>
                        <select id="countries" className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" value={product.categoryId} onChange={changeHandler('categoryId')}>
                            <option selected value="0">Choose a Category</option>
                            {
                                categories.map((o: ICategory) => {
                                    return (
                                        <option value={o.id}>{o.name}</option>
                                    )
                                })
                            }
                        </select>
                    </div>
                    <div className="mb-6">
                        <label className="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Select Variant</label>
                        <select id="countries" className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" value={product.variantId} onChange={changeHandler('variantId')}>
                            <option selected value="0">Choose a Variant</option>
                            {
                                variants.map((o: IVariant) => {
                                    return (
                                        <option value={o.id}>{o.name}</option>
                                    )
                                })
                            }
                        </select>
                    </div>
                <div className="mb-6">
                        <label className="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Initial</label>
                        <input readOnly={command == ECommand.changeStatus} type="text" id="initial" className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" required value={product.initial} onChange={changeHandler('initial')}/>
                    </div>
                    <div className="mb-6">
                        <label className="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Name</label>
                        <input readOnly={command == ECommand.changeStatus} type="text" id="name" className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" required value={product.name} onChange={changeHandler('name')}/>
                    </div>
                    <div className="mb-6">
                        <label className="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Description</label>
                        <input readOnly={command == ECommand.changeStatus} type="text" id="name" className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" required value={product.description} onChange={changeHandler('description')}/>
                    </div>
                    <div className="mb-6">
                        <label className="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Price</label>
                        <input readOnly={command == ECommand.changeStatus} type="number" id="name" className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" required value={product.price} onChange={changeHandler('price')}/>
                    </div>
                    <div className="mb-6">
                        <label className="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Stock</label>
                        <input readOnly={command == ECommand.changeStatus} type="number" id="name" className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" required value={product.stock} onChange={changeHandler('stock')}/>
                    </div>
                    {
                        command == ECommand.changeStatus ?
                        (                                   
                            <div className="flex items-start mb-6">
                                <div className="flex items-center h-5">
                                    <input type="checkbox" value="" className="w-4 h-4 border border-gray-300 rounded bg-gray-50 focus:ring-3 focus:ring-blue-300 dark:bg-gray-700 dark:border-gray-600 dark:focus:ring-blue-600 dark:ring-offset-gray-800 dark:focus:ring-offset-gray-800" required checked={product.active} onChange={checkBoxHandler('active')}/>
                                </div>
                                <label className="ms-2 text-sm font-medium text-gray-900 dark:text-gray-700">Is Active</label>
                            </div>
                        ) : null
                    }
                </form>

            </>
        )
    }
}
