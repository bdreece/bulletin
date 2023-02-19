import type { FragmentType } from '@generated/graphql';
import { graphql } from '@generated/graphql';

export const ErrorFragment = graphql(`
  fragment Error on Error {
    message
  }
`);

export type ErrorFragmentType = FragmentType<typeof ErrorFragment>;
