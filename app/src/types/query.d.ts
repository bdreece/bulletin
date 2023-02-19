import type { TypedDocumentNode } from '@graphql-typed-document-node/core';
export type InferVariables<TNode extends any> = TNode extends TypedDocumentNode<
  _,
  infer TVariables
>
  ? TVariables
  : never;

export type InferData<TNode extends any> = TNode extends TypedDocumentNode<
  infer TData,
  _
>
  ? TData
  : never;
