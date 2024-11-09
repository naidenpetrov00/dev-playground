import { useObservableState } from "observable-hooks";
import { usePokemon } from "./store";

// Subscription wiht observable-hooks package

const Deck = () => {
  const { deck$ } = usePokemon();
  const deck = useObservableState(deck$, []);

  return (
    <div>
      <h4>Deck</h4>
      <div>
        {deck.map((card) => (
          <div key={card.id} style={{ display: "flex" }}>
            <img
              src={`https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/${card.id}.png`}
              alt={card.name}
            />
            <div>
              <h5>{card.name}</h5>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
};

export default Deck;
