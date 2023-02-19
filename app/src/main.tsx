import React from 'react';
import ReactDOM from 'react-dom/client';
import {
  ApolloClient,
  InMemoryCache,
  ApolloProvider,
  from,
  HttpLink,
  ApolloLink,
} from '@apollo/client';
import { createBrowserRouter, RouterProvider } from 'react-router-dom';
import { MantineProvider } from '@mantine/core';
import { AccessTokenProvider, ProfileProvider } from '~/components';
import Layout from './layout';
import Home from './pages/Home';
import Documents from './pages/documents';

const link = from([
  new ApolloLink((operation, next) => {
    const accessToken = sessionStorage.getItem('BULLETIN_ACCESS_TOKEN');
    operation.setContext({
      headers: {
        authorization: accessToken ? `Bearer ${accessToken}` : undefined,
      },
    });
    return next(operation);
  }),
  new HttpLink({
    uri: 'http://localhost:5052/graphql',
    credentials: 'include',
  }),
]);

const client = new ApolloClient({
  link,
  cache: new InMemoryCache(),
});

const router = createBrowserRouter([
  {
    path: '/',
    element: <Layout />,
    children: [
      { index: true, element: <Home /> },
      {
        path: '/documents',
        element: <Documents />,
      },
    ],
  },
]);

const root = document.querySelector('#root')!;
ReactDOM.createRoot(root).render(
  <React.StrictMode>
    <ApolloProvider client={client}>
      <AccessTokenProvider>
        <ProfileProvider>
          <MantineProvider
            withGlobalStyles
            withNormalizeCSS
          >
            <RouterProvider router={router} />
          </MantineProvider>
        </ProfileProvider>
      </AccessTokenProvider>
    </ApolloProvider>
  </React.StrictMode>,
);
