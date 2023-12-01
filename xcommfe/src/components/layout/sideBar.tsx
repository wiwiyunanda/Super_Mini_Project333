import React from 'react';
import { Link, Route, Router, Routes } from 'react-router-dom';

import { Home } from '../home';
import { Category } from '../category';
import { Variant } from '../variant';
import { Gallery } from '../gallery';
import { Product } from '../product';
import { Authentication } from '../auth';
import { ProtectedRoute } from './protectedRoute';
import { AuthService } from '../../services/authService';
import Order from '../order/order';

export default class SideBar extends React.Component {
    render() {
        return (
            <div className="w-full h-screen flex">
                <div className="flex flex-col h-screen p-3 bg-gray-800 shadow w-60">
                    <div className="space-y-3">
                        <div className="flex items-center">
                            <h2 className="text-xl font-bold text-white">XCommerce</h2>
                        </div>
                        <div className="flex-1">
                            <ul className="pt-2 pb-4 space-y-1 text-sm">
                                <li className="rounded-sm">
                                    <a
                                        href="#"
                                        className="flex items-center p-2 space-x-3 rounded-md"
                                    >
                                        <span className="text-gray-100"><Link to="/">Home</Link></span>
                                    </a>
                                </li>
                                {
                                    AuthService.getToken() ?
                                        (<>
                                            <li className="rounded-sm">
                                                <a
                                                    href="#"
                                                    className="flex items-center p-2 space-x-3 rounded-md"
                                                >
                                                    <span className="text-gray-100"><Link to="/categories">Categories</Link></span>
                                                </a>
                                            </li>
                                            <li className="rounded-sm">
                                                <a
                                                    href="#"
                                                    className="flex items-center p-2 space-x-3 rounded-md"
                                                >
                                                    <span className="text-gray-100"><Link to="/variants">Variants</Link></span>
                                                </a>
                                            </li>
                                            <li className="rounded-sm">
                                                <a
                                                    href="#"
                                                    className="flex items-center p-2 space-x-3 rounded-md"
                                                >
                                                    <span className="text-gray-100"><Link to="/products">Products</Link></span>
                                                </a>
                                            </li>
                                            <li className="rounded-sm">
                                                <a
                                                    href="#"
                                                    className="flex items-center p-2 space-x-3 rounded-md"
                                                >
                                                    <span className="text-gray-100"><Link to="/galleries">Galleries</Link></span>
                                                </a>
                                            </li>
                                            <li className="rounded-sm">
                                                <a
                                                    href="#"
                                                    className="flex items-center p-2 space-x-3 rounded-md"
                                                >
                                                    <span className="text-gray-100"><Link to="/orders">Order</Link></span>
                                                </a>
                                            </li>
                                        </>) : null
                                }

                                <li className="rounded-sm">
                                    <a
                                        href="#"
                                        className="flex items-center p-2 space-x-3 rounded-md"
                                    >
                                        <span className="text-gray-100"><Link to="/auth">Login</Link></span>
                                    </a>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
                <div className="w-full grid grid-cols-1 gap-3 px-8 h-1/2 text-center">
                    <Routes>
                        <Route path="/" Component={Home} />
                        <Route element={<ProtectedRoute />}>
                            <Route path="/categories" element={<Category />} />
                            <Route path="/variants" element={<Variant />} />
                            <Route path="/products" element={<Product />} />
                            <Route path="/galleries" element={<Gallery />} />
                            <Route path="/orders" element={<Order />} />
                        </Route>
                        <Route path="/auth" element={<Authentication />} />
                    </Routes>
                </div>
            </div>
        )
    }
}
