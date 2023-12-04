import React from 'react';
import { AccountModel } from '../models/accountModel';

interface IProps {
    logged: boolean;
    account: AccountModel;
}

interface IState {
}

export default class Header extends React.Component<IProps, IState> {
    render() {
        const { account, logged } = this.props;
        return (
            <div>
                <div className="bg-gray-800 p-3 flex flex-col lg:flex-row list-none lg:ml-auto">
                    <div className="container mx-auto space-y-3">
                        <h2 className="text-xl font-bold text-white">XCommerce</h2>
                    </div>
                    <div className="container mx-auto space-y-3">
                        <h2 className="container text-sm text-white">
                            Hi, {logged ? account.userName : 'Guest'}
                        </h2>
                    </div>
                </div>
            </div>
        )
    }
}
