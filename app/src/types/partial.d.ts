export type Impartial<T extends any> = T extends Partial<infer TOriginal>
  ? TOriginal
  : never;
