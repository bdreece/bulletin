import {
  ApolloClient,
  InMemoryCache,
  from,
  HttpLink,
  ApolloLink,
} from '@apollo/client';

import { ACCESS_TOKEN_KEY } from '~/components/providers/AccessTokenProvider';

const authLink = new ApolloLink((operation, next) => {
  const accessToken = localStorage.getItem(ACCESS_TOKEN_KEY);
  operation.setContext({
    headers: {
      authorization: accessToken ? `Bearer ${accessToken}` : undefined,
      'X-Apollo-Tracing': 1,
    },
  });
  return next(operation);
});

const httpLink = new HttpLink({
  uri: 'http://localhost:8080/graphql',
  credentials: 'include',
});

const link = from([authLink, httpLink]);

const client = new ApolloClient({
  link,
  cache: new InMemoryCache(),
});

export default client;
