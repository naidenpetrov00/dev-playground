import { useState } from "react";

export function c(size) {
  return useState(() => new Array(size))[0];
}
