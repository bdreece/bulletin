export type Nullable<T extends any> = {
  [TKey in keyof T]: T[TKey] | null;
};

export type NonNullable<T extends any> = T extends Nullable<infer TOriginal>
  ? TOriginal
  : never;
