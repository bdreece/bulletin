import { Impartial } from './partial';
import { NonNullable } from './nullable';
export type Required<T extends any> = Impartial<NonNullable<T>>;
