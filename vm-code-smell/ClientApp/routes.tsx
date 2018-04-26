import * as React from 'react';
import { Route } from 'react-router-dom';
import { Layout } from './components/Layout';
import Home from './components/Home';
import FetchData from './components/FetchData';
import Timesheet from "./components/Timesheet";

export const routes = <Layout>
    <Route exact path='/' component={ Home } />
    <Route path='/fetchdata/:startDateIndex?' component={ FetchData } />
    <Route path='/timesheet' component={ Timesheet }/>
</Layout>;
