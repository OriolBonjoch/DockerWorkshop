import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { WildPokemonsPage } from './components/WildPokemonsPage';
import { CatchedPokemonsPage } from './components/CatchedPokemonsPage';

import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/wild-pokemons' component={WildPokemonsPage} />
        <Route path='/bag-pokemons' component={CatchedPokemonsPage} />
      </Layout>
    );
  }
}
