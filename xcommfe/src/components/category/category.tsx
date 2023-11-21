import React from 'react';

import {ICategory} from '../../interfaces/iCategory';
import { CategoryService } from '../../services/categoryServices';

import Form from './form';

interface IProps {
}

interface IState {
    categories: ICategory[];
    category: ICategory;
    showModal: boolean;
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
            showModal: false
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

    submitHandler = async () => {
        const { category } = this.state;
        const result = await CategoryService.post(category);
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
        }
    }

    render() {
        const { categories, category, showModal } = this.state;
        return (           
            <div>
            <div className="text-left text-3xl pt-5 text-center">Categories</div>
            <span>{JSON.stringify(category)}</span>
            <div className="flex" aria-label="Button">
                <button className="my-8 justify-start h-8 px-4 text-green-100 transition-colors duration-150 
                 bg-green-700 rounded focus:shadow-outline hover:bg-green-800" onClick={() => this.setShowModal(true)}>Create New </button>
            </div>
            <table className="w-full text-sm text-left  text-gray-50">
                <thead className="text-xs text-gray-300 uppercase bg-gray-50">
                    <tr className="border-b bg-gray-700 border-gray-700">
                        <th scope="col" className="px-6 py-3 w-14 h-14">
                            {/* grow flex-none  */}
                            Id
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
                        categories?.map((cat: any) => {
                            return <tr className="border-b bg-gray-800 border-gray-700">
                                <td scope="row" className="px-6 py-4 font-medium text-white-900 whitespace-nowrap text-white">
                                    {cat.id}
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
            {
                showModal ? (
                    <div className="flex justify-center items-center overflow-x-hidden overflow-y-auto fixed inset-0 z-50 outline-none focus:outline-none ">
                        <div className="relative w-auto my-6 mx-auto max-w-3xl ">
                            <div className="border-0 rounded-lg shadow-lg relative flex flex-col w-full bg-white outline-none focus:outline-none dark:bg-gray-900">
                                <div className="flex items-start justify-between p-5 border-b border-solid border-gray-300 rounded-t ">
                                    <h3 className="text-3xl text-gray-900 dark:text-white">Create</h3>
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
                                    <Form category={category} changeHandler={this.changeHandler} checkBoxHandler={this.checkBoxHandler}/>
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