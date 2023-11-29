import { Outlet, Navigate } from 'react-router-dom';
import { AuthService } from '../../services/authService';

export const ProtectedRoute = () => {
    return (
        AuthService.getToken() ? <Outlet/> : <Navigate to ="/auth" />
    )
}