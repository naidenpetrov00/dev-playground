import Deck from "./Deck";
import { Search } from "./Search";
import { PokemonProvider } from "./store";

function App() {
  return (
    <PokemonProvider>
      <div style={{ display: "flex", grid: "1fr 1fr" }}>
        <Search />
        <Deck />
      </div>
    </PokemonProvider>
  );
}

export default App;
