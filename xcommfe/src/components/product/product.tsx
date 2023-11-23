import React from 'react';

import {IProduct} from '../../interfaces/iProduct';
import { ProductService } from '../../services/productServices';
import { VariantService } from '../../services/variantServices';

import Form from './form';
import { ECommand } from '../../enums/eCommand';
import { IVariant } from '../../interfaces/iVariant';


interface IProps {
}
interface IState {
    variants: IVariant[];
    products: IProduct[];
    product: IProduct;
    showModal: boolean;
    command: ECommand;
}

export default class Product extends React.Component<IProps, IState> {
    constructor(props: IProps) {
        super(props);
        this.state = {
            variants: [],
            products: [],
            product: {
                id: 0,
                variantId: 0,
                initial: '',
                name: '',
                description: '',
                price: 0,
                stock: 0,
                active: false
            },
            showModal: false,
            command: ECommand.create
        }
    }

    componentDidMount(): void {
        this.loadProducts();
    }
    loadProducts = async () => {
        const result = await ProductService.getAll();
        if (result.success) {
            const catResult = await VariantService.getAll();
            if (catResult.success) {
                this.setState({
                    variants: catResult.result
                });
            }
            this.setState({
                products: result.result
            });
        } else {
            alert('Error: ' + result.result);
        }
    }

    setShowModal = (val: boolean) => {
        this.setState({
            showModal: val
        })
        console.log(this.state.showModal);
    }

    changeHandler = (name: any) => (event: any) => {
        this.setState({
            product: {
              ...this.state.product,
                [name]: event.target.value
            }
        })
    }

    checkBoxHandler = (name: any) => (event: any) => {
        this.setState({
            product: {
              ...this.state.product,
                [name]: event.target.checked
            }
        })
    }

    createCommand = (command: string) =>{
        this.setState ({
            showModal: true,
            product: {id : 0, variantId: 0, initial: '',  name: '', description: '', price:0, stock:0, active:false},
            command: ECommand.create,
            
        })
        //this.setShowModal(true);
    }
    updateCommand = async (id: number) =>{
        await ProductService.getById(id)
            .then((result) => {
                if (result.success){
                    this.setState({
                        showModal: true,
                        product: result.result,
                        command: ECommand.update                               
                    });
                    this.loadProducts();
                } else{
                    alert("error" + result.result);
                }
            })
            .catch(error => {
                alert("error" + error);
            });
    };

    changeStatusCommand = async (id: number) => {
        await ProductService.getById(id)
            .then(result => {
                if (result.success) {
                    this.setState({
                        showModal: true,
                        product: result.result,
                        command: ECommand.changeStatus
                    })
                } else {
                    alert('Error result ' + result.result);
                }
            })
            .catch(error => {
                alert('Error error' + error);
            })
    };

    submitHandler = async () => {
        const { product } = this.state;
        const {command} = this.state;
        if (command == ECommand.create) {
            await ProductService.post(this.state.product)
            .then(result => {
                if (result.success){
                    this.setState({
                        showModal: false,
                        product: {
                            id: 0,
                            variantId : 0,
                            initial: '',
                            name: '',
                            description: '',
                            price:0,
                            stock:0,
                            active: true
                        }
        
                    })
                    this.loadProducts();
                } else{
                    alert('Error result' + result.result);
                }
            })
            .catch(error => {
                alert('Error error' + error);
            })
        } else if (command == ECommand.update) {
            
            await ProductService.update(product.id, product)
            .then(result => {
                if (result.success){
                    this.setState({
                        showModal: false,
                        product: {
                            id: 0,
                            variantId : 0,
                            initial: '',
                            name: '',
                            description: '',
                            price:0,
                            stock:0,
                            active: true
                        }        
                    })
                    this.loadProducts();
                } else{
                    alert('Error result' + result.result);
                }
            })
            .catch(error => {
                alert('Error error' + error);
            })

        } else if (command == ECommand.changeStatus) {
            await ProductService.changeStatus(product.id, product.active)
            .then(result => {
                if (result.success) {
                    this.setState({
                        showModal: false,
                        product: {
                            id: 0,
                            variantId: 0,
                            initial: '',
                            name: '',
                            description: '',
                            price:0,
                            stock:0,
                            active: true
                        }
                    })
                    this.loadProducts();
                } else {
                    alert('Error result ' + result.result);
                }
            })
            .catch(error => {
                alert('Error error' + error);
            })
        }
    }

    render() {
        const { variants, products,  showModal, command } = this.state;
        return (           
            <div>
            <div className="text-left text-3xl pt-5 text-center">Products</div>
            <span>{JSON.stringify(products)}</span>
            <div className="flex" aria-label="Button">
                <button className="my-8 justify-start h-8 px-4 text-green-100 transition-colors duration-150 
                 bg-green-700 rounded focus:shadow-outline hover:bg-green-800">Create New </button>
            </div>
            <table className="w-full text-sm text-left text-gray-50">
                <thead className="text-xs text-gray-300 uppercase bg-gray-50">
                    <tr className="border-b bg-gray-700 border-gray-700">
                        <th scope="col" className="px-6 py-3 w-14 h-14">
                            VariantId
                            </th>    
                        <th scope="col" className="px-6 py-3 w-14 h-14">
                            Initial
                        </th>
                        <th scope="col" className="px-6 py-3 w-14 h-14">
                            Name
                        </th>
                        <th scope="col" className="px-6 py-3 w-14 h-14">
                            Description
                        </th>
                        <th scope="col" className="px-6 py-3 w-14 h-14">
                            Price
                        </th>
                        <th scope="col" className="px-6 py-3 w-14 h-14">
                            Stock
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
                        products?.map((cat: IProduct) => {
                            return <tr key={cat.id} className="border-b bg-gray-800 border-gray-700">                            
                                <td className="px-6 py-4">
                                    {cat.variantId}
                                </td>
                                <td className="px-6 py-4">
                                    {cat.initial}
                                </td>
                                <td className="px-6 py-4">
                                    {cat.name}
                                </td>
                                <td className="px-6 py-4 ">
                                    {cat.description}
                                </td>
                                <td className="px-6 py-4 ">
                                    {cat.price}
                                </td>
                                <td className="px-6 py-4 ">
                                    {cat.stock}
                                </td>
                                <td className="px-6 py-4">
                                    <div className="flex items-center">
                                    <input checked={cat.active} id="checked-checkbox" type="checkbox" className="w-4 h-4 text-blue-600 bg-gray-100 border-gray-300 rounded focus:ring-blue-500 dark:focus:ring-blue-600 dark:ring-offset-gray-800 focus:ring-2 dark:bg-gray-700 dark:border-gray-600" />
                                    </div>
                                </td>
                                <td className="px-4 py-4">
                                <div className="inline-flex" role="group" aria-label="Button group">
                                            <button className="h-8 px-4 text-green-100 transition-colors duration-150 bg-green-700 rounded-l-lg focus:shadow-outline hover:bg-green-800" onClick={() => this.updateCommand(cat.id)}>Edit</button>
                                            <button className="h-8 px-4 text-blue-100 transition-colors duration-150 bg-blue-700 rounded-r-lg focus:shadow-outline hover:bg-blue-800" onClick={() => this.changeStatusCommand(cat.id)}>Status</button>
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