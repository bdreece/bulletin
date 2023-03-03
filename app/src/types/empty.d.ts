export type Empty<T extends any> = T extends Record<string, never>
  ? undefined
  : T;
