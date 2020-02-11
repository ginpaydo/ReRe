import * as React from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import Home from './components/Home';
import Counter from './components/Counter';
import NazoMap from './components/leaflet/NazoMap';
//import FetchData from './components/FetchData';
//import NazoChart from './components/chart/NazoChart';
//import NazoMap from './components/leaflet/NazoMap';

import './custom.css'
import './leaflet.css'
import 'leaflet/dist/leaflet.css'

/**
 * ここでデフォルトの画面表示を設定する
 * 現在はLayout.tsxを呼び出すのみ
 * ルーティングも設定する
 */
export default () => (
    // Routeタグを使って、tsxコンポーネントでルーティングアドレスと遷移先を指定する。
    <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/counter' component={Counter} />
        <Route path='/counter34' render={props => <Counter aaaa={34} {...props} />} />
        <Route path='/nazo-map' component={NazoMap} />
        {/*
        <Route path='/fetch-data/:startDateIndex?' component={FetchData} />
        <Route path='/nazo-chart' component={NazoChart} />
        <Route path='/nazo-map' component={NazoMap} />*/}
    </Layout>
);