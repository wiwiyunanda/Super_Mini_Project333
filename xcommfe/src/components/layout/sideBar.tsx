import React from 'react';
import { Link, Route, Router, Routes, RouteProps } from 'react-router-dom';

import { Home } from '../home';
import { Category } from '../category';
import { Variant } from '../variant';
import { Gallery } from '../gallery';
import { Product } from '../product';
import { Authentication } from '../auth';
import { ProtectedRoute } from './protectedRoute';
import Order from '../order/order';
import Header from './header';
import { AccountModel } from '../models/accountModel';
import { Restricted } from './restricted';

interface IProps {
    logged: boolean;
    changeLoggedHandler: any;
    account: AccountModel
}

interface IState {
}

export default class SideBar extends React.Component<IProps, IState> {
    constructor(props: IProps) {
        super(props);
    }

    render() {
        const { logged, account, changeLoggedHandler } = this.props;
        return (
            <>
                <Header account={account} logged={logged} />
                <div className="w-full h-screen flex">
                    <div className="flex flex-col h-screen p-3 bg-gray-800 shadow w-60">
                        <div className="space-y-3">
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
                                        logged ?
                                            (
                                                <>
                                                    {account.roles.map(o => {
                                                        return (
                                                            <li className="rounded-sm">
                                                                <a
                                                                    href="#"
                                                                    className="flex items-center p-2 space-x-3 rounded-md"
                                                                >
                                                                    <span className="text-gray-100"><Link to={`/${o}`}>{o}</Link></span>
                                                                </a>
                                                            </li>
                                                        )
                                                    })}
                                                </>
                                            ) : null
                                    }

                                    <li className="rounded-sm">
                                        <a
                                            href="#"
                                            className="flex items-center p-2 space-x-3 rounded-md"
                                        >
                                            <span className="text-gray-100"><Link to="/auth">{logged ? 'Log Out' : 'Log In'}</Link></span>
                                        </a>
                                    </li>
                                </ul>
                            </div>
                        </div>
                    </div>
                    <div className="w-full grid grid-cols-1 gap-3 px-8 h-1/2 text-center">
                        <Routes>
                            <Route path="/" Component={Home} />
                            <Route element={<ProtectedRoute isAllowed={account.roles.indexOf("Categories") > -1} />}>
                                <Route path="/categories" element={<Category />} />
                            </Route>
                            <Route element={<ProtectedRoute isAllowed={account.roles.indexOf("Variants") > -1} />}>
                                <Route path="/variants" element={<Variant />} />
                            </Route>
                            <Route element={<ProtectedRoute isAllowed={account.roles.indexOf("Products") > -1} />}>
                                <Route path="/products" element={<Product />} />
                            </Route>
                            <Route element={<ProtectedRoute isAllowed={account.roles.indexOf("Galleries") > -1} />}>
                                <Route path="/galleries" element={<Gallery />} />
                            </Route>
                            <Route element={<ProtectedRoute isAllowed={account.roles.indexOf("Orders") > -1} />}>
                                <Route path="/orders" element={<Order />} />
                            </Route>
                            <Route
                                path="/auth"
                                element={<Authentication logged={logged} changeLoggedHandler={changeLoggedHandler} />}
                            />
                            <Route path="/restricted" Component={Restricted} />
                        </Routes>
                    </div>
                </div>
            </>
        )
    }
}
