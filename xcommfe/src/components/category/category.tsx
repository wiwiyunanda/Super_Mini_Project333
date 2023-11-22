import React from 'react';

import {ICategory} from '../../interfaces/iCategory';
import { CategoryService } from '../../services/categoryServices';

import Form from './form';
import { ECommand } from '../../enums/eCommand';

interface IProps {
}

interface IState {
    categories: ICategory[];
    category: ICategory;
    showModal: boolean;
    command: ECommand;
}

export default class Category extends React.Component<IProps, IState> {
    constructor(props: IProps) {
        super(props);
        this.state = {
            categories: [],
            category: {
                id: 0,
                initial: '',
                name: '',
                active: false
            },
            showModal: false,
            command: ECommand.create
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

    setShowModal = (val: boolean) => {
        this.setState({
            showModal: val
        })
        console.log(this.state.showModal);
    }

    changeHandler = (name: any) => (event: any) => {
        this.setState({
            category: {
              ...this.state.category,
                [name]: event.target.value
            }
        })
    }

    checkBoxHandler = (name: any) => (event: any) => {
        this.setState({
            category: {
              ...this.state.category,
                [name]: event.target.checked
            }
        })
    }

    createCommand = (command: string) =>{
        this.setState ({
            showModal: true,
            category: {id : 0, initial: '',  name: '', active:false},
            command: ECommand.create,
            
        })
        //this.setShowModal(true);
    }
    updateCommand = async (id: number) =>{
        await CategoryService.getById(id)
            .then((result) => {
                if (result.success){
                    this.setState({
                        showModal: true,
                        category: result.result,
                        command: ECommand.edit                               
                    });
                    this.loadCategories();
                } else{
                    alert("error" + result.result);
                }
            })
            .catch(error => {
                alert("error" + error);
            });
    };

    changeStatusCommand = async (id: number) => {
        await CategoryService.getById(id)
            .then(result => {
                if (result.success) {
                    this.setState({
                        showModal: true,
                        category: result.result,
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
        const { category } = this.state;
        const {command} = this.state;
        if (command == ECommand.create) {
            await CategoryService.post(this.state.category)
            .then(result => {
                if (result.success){
                    this.setState({
                        showModal: false,
                        category: {
                            id: 0,
                            initial: '',
                            name: '',
                            active: true
                        }
        
                    })
                    this.loadCategories();
                } else{
                    alert('Error result' + result.result);
                }
            })
            .catch(error => {
                alert('Error error' + error);
            })
        } else if (command == ECommand.edit) {
            
            await CategoryService.update(category.id, category)
            .then(result => {
                if (result.success){
                    this.setState({
                        showModal: false,
                        category: {
                            id: 0,
                            initial: '',
                            name: '',
                            active: true
                        }        
                    })
                    this.loadCategories();
                } else{
                    alert('Error result' + result.result);
                }
            })
            .catch(error => {
                alert('Error error' + error);
            })

        } else if (command == ECommand.changeStatus) {
            await CategoryService.changeStatus(category.id, category.active)
            .then(result => {
                if (result.success) {
                    this.setState({
                        showModal: false,
                        category: {
                            id: 0,
                            initial: '',
                            name: '',
                            active: true
                        }
                    })
                    this.loadCategories();
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
        const { categories, category, showModal, command } = this.state;
        return (           
            <div>
            <div className="text-left text-3xl pt-5 text-center">Categories</div>
            <span>{JSON.stringify(category)}</span>
            <div className="flex" aria-label="Button">
                <button className="my-8 justify-start h-8 px-4 text-green-100 transition-colors duration-150 
                 bg-green-700 rounded focus:shadow-outline hover:bg-green-800" onClick={() => this.createCommand('create')}>Create New </button>
            </div>
            <table className="w-full text-sm text-left  text-gray-50">
                <thead className="text-xs text-gray-300 uppercase bg-gray-50">
                    <tr className="border-b bg-gray-700 border-gray-700">
                       
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
                        categories?.map((cat: ICategory) => {
                            return <tr key={cat.id} className="border-b dark:bg-gray-800 dark:border-gray-700">
                                    <td className="px-6 py-4">
                                        {cat.initial}
                                    </td>
                                    <td className="px-6 py-4">
                                        {cat.name}
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
            {
                showModal ? (
                    <div className="flex justify-center items-center overflow-x-hidden overflow-y-auto fixed inset-0 z-50 outline-none focus:outline-none ">
                            <div className="relative w-auto my-6 mx-auto max-w-3xl ">
                                <div className="border-0 rounded-lg shadow-lg relative flex flex-col w-full bg-white outline-none focus:outline-none dark:bg-gray-900">
                                    <div className="flex items-start justify-between p-5 border-b border-solid border-gray-300 rounded-t ">
                                        <h3 className="text-3xl text-gray-900 dark:text-white">{command.valueOf()}</h3>
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
                                        <Form category={category} command={command} changeHandler={this.changeHandler} checkBoxHandler={this.checkBoxHandler} />
                                    </div>
                                    <div className="flex items-center justify-end p-6 border-t border-solid border-blueGray-200 rounded-b" role="group" aria-label="Button group">
                                        <button className="my-8 justify-start h-8 px-4 text-green-100 transition-colors duration-150 bg-green-700 rounded-l-lg focus:shadow-outline hover:bg-green-800" onClick={() => this.setShowModal(false)}>Close</button>
                                        <button className="my-8 justify-start h-8 px-4 text-blue-100 transition-colors duration-150 bg-blue-700 rounded-r-lg focus:shadow-outline hover:bg-blue-800" onClick={() => this.submitHandler()}>Submit</button>
                                    </div>
                                </div>
                            </div>
                    </div>
                ) : null
            }
        </div>
        )
    }
}