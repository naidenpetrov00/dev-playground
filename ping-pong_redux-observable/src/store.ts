import { createStore, applyMiddleware } from "redux";
import { createEpicMiddleware } from "redux-observable";
import { pingEpic } from "./epics";

const epicMiddleware = createEpicMiddleware(pingEpic);

export default createStore(pingReducer, applyMiddleware(epicMiddleware));
