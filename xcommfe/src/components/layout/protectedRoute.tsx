import React, {Component} from 'react';
import { Route, Navigate } from 'react-router-dom';
import { AuthService } from '../../services/authService';

export const ProtectedRoute = ({component, ...rest}: any) => {
    return(
        <Route
            {...rest}
            render ={
                (props: any) => {
                    if (AuthService.getToken()) {
                        return <Component {...props} />
                    } else {
                        return <Navigate to={{
                            pathname: '/restricted'
                        }}/>
                    }
                }
            }
        />
    )
}