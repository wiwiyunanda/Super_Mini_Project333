import React from 'react';
import { Link, Route, Routes } from 'react-router-dom'

import { Home } from '../home'
import {Category} from '../category'
import {Variant} from '../variant'
import {Product} from '../product'
;

export default class SideBar extends React.Component {
    render() {
        return (
            <div>
                <div>
                    <Link to='/'>Home </Link>
                    <Link to='/category'>Category</Link>
                    <Link to='/variant'>Variant</Link>
                    <Link to='/product'>Product</Link>

                </div>
                <div>
                    <Routes>
                        <Route path='/' Component={Home} />
                        <Route path='/category' Component={Category} />
                        <Route path='/variant' Component={Variant} />
                        <Route path='/product' Component={Product} />
                    </Routes>
                </div>               
            </div>
        )
    }
}