import {
  LoginDocument,
  RefreshDocument,
  RegisterDocument,
  SelfDocument,
} from '@generated/graphql/graphql';
import { createQuery, createMutation } from './query';

export const useLogin = createMutation(LoginDocument);
export const useRegister = createMutation(RegisterDocument);
export const useRefresh = createQuery(RefreshDocument);
export const useSelf = createQuery(SelfDocument);
