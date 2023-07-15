import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import {RouterProvider} from 'react-router-dom';
import reportWebVitals from './reportWebVitals';
import 'bootstrap/dist/css/bootstrap.min.css';
import router from './routes/routes';
import {UserProvider} from './contexts/UserContext'


const root = document.getElementById('root');
ReactDOM.render(
  <React.StrictMode>
    <UserProvider>
    <RouterProvider router={router} />
    </UserProvider>
  </React.StrictMode>,root
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
