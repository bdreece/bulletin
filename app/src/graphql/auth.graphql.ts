import { graphql } from '@generated/graphql';
import { createMutation, createQuery } from '~/hooks/query';

export const useLogin = createMutation(
  graphql(`
    mutation Login($input: LoginInput!) {
      login(input: $input) {
        result {
          accessToken
          expiration
        }
        errors {
          ...Error
        }
      }
    }
  `),
);

export const useRefresh = createQuery(
  graphql(`
    query Refresh {
      refresh {
        error
        result {
          accessToken
          expiration
        }
      }
    }
  `),
);

export const useSelf = createQuery(
  graphql(`
    query Self {
      self {
        id
        name
        email
        roles
      }
    }
  `),
);
