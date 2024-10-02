import "./App.css";
import Header from "./components/Header";
import { useState } from "react";
import Counter from "./components/Counter";

function App() {
  const [count1, setCount1] = useState(0);
  const [count2, setCount2] = useState(0);

  return (
    <div className="App">
      <Header />
      <Counter count={count1} setCount={setCount1} countNum={1} />
      <Counter count={count2} setCount={setCount2} countNum={2} />
    </div>
  );
}

export default App;
