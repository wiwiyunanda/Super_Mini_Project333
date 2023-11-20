import React from 'react';  
import './App.css';
import { BrowserRouter } from 'react-router-dom';

import {Header} from './components/layout';


function App() {
  return (
    <BrowserRouter>
      <Header/>
    </BrowserRouter>
  );
}

export default App;
