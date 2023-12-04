import React, { useState } from 'react';
import './App.css';
import { BrowserRouter } from 'react-router-dom';

import SideBar from './components/layout/sideBar';
import { AuthService } from './services/authService';
import { AccountModel } from './components/models/accountModel';

interface IProps { }

interface IState {
  logged: boolean;
  account: AccountModel;
}

export default class App extends React.Component<IProps, IState> {

  constructor(props: IProps) {
    super(props);
    this.state = {
      logged: false,
      account: new AccountModel()
    }
  }

  componentDidMount = async () => {
    await this.setState({
      logged: AuthService.getToken() ? true : false,
      account: AuthService.getAccount()
    })
  }

  handleChangeStatus = (val: boolean) => {
    this.setState({
      logged: val,
      account: val ? AuthService.getAccount() : new AccountModel()
    })
  }

  render() {
    return (
      <BrowserRouter>
        <SideBar account={this.state.account} logged={this.state.logged} changeLoggedHandler={this.handleChangeStatus} />
      </BrowserRouter>
    );
  }
}

// export default App;
