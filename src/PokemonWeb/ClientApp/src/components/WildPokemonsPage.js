import React, { useCallback, useEffect, useState } from 'react';
import { ReactComponent as Pokeball } from '../icons/ball_icon.svg';
import './WildPokemonsPage.css';
import { Card, CardColumns, CardImg, CardBody, CardTitle, CardSubtitle } from 'reactstrap';

function WildPokemons() {
  const [pokemons, setPokemons] = useState([]);
  const [loading, setLoading] = useState(true);

  const catchPokemon = useCallback(async (id) => {
    const response = await fetch(`api/pokemon/catch/${id}`, { method: 'PUT' });
    const pokemonData = await response.json();

    if (pokemonData.catched) {
      setPokemons(prev => prev.filter(pokemon => pokemon.id !== id));
    }
  }, []);

  useEffect(() => {
    async function fetchPokemons() {
      const response = await fetch('api/pokemon');
      const data = await response.json();

      const pokemons = data.filter(p => !p.fainted);
      setPokemons(pokemons.filter(pokemon => !pokemon.catched));
      setLoading(false);
    }

    fetchPokemons();
  }, []);

  if (loading) {
    return <p><em>Loading...</em></p>
  }

  if (pokemons.length === 0) {
    return (<p><em>No pokemons</em></p>);
  }

  return (<CardColumns>
    {pokemons.map((pokemon, i) => {
      const imageUrl = `https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/official-artwork/${pokemon.pokepediaId}.png`;
      return <Card key={i}>
        <div className="hover-pokeball" onClick={() => catchPokemon(pokemon.id)} >
          <Pokeball />
        </div>
        <CardImg top src={imageUrl} alt={pokemon.name} />
        <CardBody>
          <CardTitle tag="h3">{pokemon.name}</CardTitle>
          <CardSubtitle tag="h6" className="mb-2 text-muted">{`${pokemon.height} Kg / ${pokemon.experience} exp`}</CardSubtitle>
        </CardBody>
      </Card>
    })}
  </CardColumns>);
}

export function WildPokemonsPage() {
  return (
    <div>
      <h1 id="tableLabel" >Wild Pokemons</h1>
      <p>This component shows a list of wild pokemons.</p>
      <WildPokemons />
    </div>
  );
}