import React from 'react';
import { inputCls, labelCls, buttonCls } from '../style/styledComponent';
import { IAuthentication } from '../../interfaces/iAuthentication';
import { AuthService } from '../../services/authService';
import { withRouter } from '../layout/withRouter';

interface IProps {
    logged: boolean;
    changeLoggedHandler: any;
    navigate: any;
}

interface IState {
    auth: IAuthentication
}

class Authentication extends React.Component<IProps, IState> {

    newAuth: IAuthentication = { userName: '', password: '' };
    constructor(props: IProps) {
        super(props);
        this.state = {
            auth: this.newAuth
        }
        this.props.changeLoggedHandler(false);
    }

    changeHandler = (name: any) => (event: any) => {
        this.setState({
            auth: {
                ...this.state.auth,
                [name]: event.target.value
            }
        })
    }

    componentDidMount(): void {
        AuthService.logout();
    }

    handleSubmit = async () => {
        const { auth } = this.state;
        const { changeLoggedHandler } = this.props;
        AuthService.login(auth)
            .then(result => {
                if (result.success) {
                    console.log(result.result);
                    changeLoggedHandler(true);
                    this.props.navigate('/');
                } else {
                    alert('Error: ' + result.result);
                }
            })
            .catch(error => {
                console.log('Error: ', error);
            })
    }

    render() {
        const { auth } = this.state;
        return (
            <>
                <div className="text-left text-3xl pt-5">Log in</div>
                <div className="border-0 rounded-lg shadow-lg relative flex flex-col bg-white outline-none focus:outline-none dark:bg-gray-900">
                    <div className="relative p-6 flex-auto">
                        <div className="mb-6">
                            <label className={labelCls}>User Name</label>
                            <input type="text" id="userName" className={inputCls} required value={auth.userName} onChange={this.changeHandler('userName')} />
                        </div>
                        <div className="mb-6">
                            <label className={labelCls}>Password</label>
                            <input type="password" id="password" className={inputCls} required value={auth.password} onChange={this.changeHandler('password')} />
                        </div>
                        <div className="flex" aria-label="Button">
                            <button className={buttonCls} onClick={this.handleSubmit}>Login</button>
                        </div>
                    </div>
                </div>
            </>
        )
    }
}

export default withRouter(Authentication);
