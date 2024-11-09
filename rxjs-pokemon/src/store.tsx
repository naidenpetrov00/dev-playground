import { createContext, PropsWithChildren, useContext } from "react";
import { BehaviorSubject, combineLatestWith, map } from "rxjs";

export interface Pokemon {
  id: number;
  name: string;
  type: string[];
  hp: number;
  attack: number;
  defense: number;
  special_attack: number;
  special_defense: number;
  speed: number;
  power?: number;
  selected?: boolean;
}

const rawPokemon$ = new BehaviorSubject<Pokemon[]>([]);

const pokemonWithPower$ = rawPokemon$.pipe(
  map((pokemon) =>
    pokemon.map((pokemonData) => ({
      ...pokemonData,
      power:
        pokemonData.hp +
        pokemonData.attack +
        pokemonData.defense +
        pokemonData.special_attack +
        pokemonData.special_defense +
        pokemonData.speed,
    }))
  )
);
const response = await fetch("/pokemon-simplified.json");
const data = await response.json();
rawPokemon$.next(data);

const selected$ = new BehaviorSubject<number[]>([]);

const pokemon$ = pokemonWithPower$.pipe(
  combineLatestWith(selected$),
  map(([pokemon, selected]) =>
    pokemon.map((p) => ({ ...p, selected: selected.includes(p.id) }))
  )
);

const deck$ = pokemon$.pipe(
  map((pokemon) => pokemon.filter((data) => data.selected))
);

const PokemonContext = createContext({ pokemon$, selected$, deck$ });

export const usePokemon = () => useContext(PokemonContext);

export const PokemonProvider: React.FunctionComponent<PropsWithChildren> = ({
  children,
}) => (
  <PokemonContext.Provider value={{ pokemon$, selected$, deck$ }}>
    {children}
  </PokemonContext.Provider>
);
