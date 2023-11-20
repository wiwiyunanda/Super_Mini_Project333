import React from 'react';
import { Link, Route, Routes } from 'react-router-dom'

import { Home } from '../home'
import {Category} from '../category';

export default class SideBar extends React.Component {
    render() {
        return (
            <div>
                <div>
                    <Link to='/'>Home </Link>
                    <Link to='/category'>Category</Link>
                </div>
                <div>
                    <Routes>
                        <Route path='/' Component={Home} />
                        <Route path='/category' Component={Category} />
                    </Routes>
                </div>               
            </div>
        )
    }
}