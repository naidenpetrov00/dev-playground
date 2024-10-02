/* eslint-disable react/prop-types */
import { memo, useMemo } from "react";

const Counter = ({ count, setCount, countNum }) => {
  console.log(`from counter ${countNum}`);

  //Check without useMemo
  let number = countNum;
  const memoNumber = useMemo(() => {
    for (let index = 1; index <= 1000000; index++) {
      number++;
    }
    console.log(number);
    return number;
  }, [number]);
  // const memoNumber = () => {
  //   for (let index = 1; index <= 1000000; index++) {
  //     number++;
  //   }
  //   console.log(number);
  //   return number;
  // };

  return (
    <div>
      <button
        onClick={() => {
          setCount(++count);
        }}
      >
        {count}
      </button>
      <p>Doubled Count: {memoNumber}</p>
    </div>
  );
};

export default Counter;
