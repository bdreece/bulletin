import type {
  MutationHookOptions,
  OperationVariables,
  QueryHookOptions,
  TypedDocumentNode,
} from '@apollo/client';
import { useQuery, useMutation } from '@apollo/client';

type QueryOptions<
  TData extends Record<string, any>,
  TVariables extends OperationVariables,
> = Omit<QueryHookOptions<TData, TVariables>, 'variables'>;

type MutationOptions<
  TData extends Record<string, any>,
  TVariables extends OperationVariables,
> = Omit<MutationHookOptions<TData, TVariables>, 'variables'>;

export const createQuery =
  <TData extends Record<string, any>, TVariables extends OperationVariables>(
    node: TypedDocumentNode<TData, TVariables>,
    defaultOptions?: QueryOptions<TData, TVariables> | undefined,
  ) =>
  (
    variables?: TVariables | undefined,
    options?: QueryOptions<TData, TVariables> | undefined,
  ) =>
    useQuery(node, {
      variables,
      ...defaultOptions,
      ...options,
    });

export const createMutation =
  <TData extends Record<string, any>, TVariables extends OperationVariables>(
    node: TypedDocumentNode<TData, TVariables>,
    defaultOptions?: MutationOptions<TData, TVariables> | undefined,
  ) =>
  (options?: MutationOptions<TData, TVariables> | undefined) =>
    useMutation(node, {
      ...defaultOptions,
      ...options,
    });
