import { Outlet, Navigate } from 'react-router-dom';
import { AuthService } from '../../services/authService';

export const ProtectedRoute = ({ isAllowed }: any) => {
    console.log(isAllowed);
    return (
        AuthService.getToken() ?
            isAllowed ? <Outlet /> : <Navigate to="/restricted" />
            : <Navigate to="/auth" />
    )
}
