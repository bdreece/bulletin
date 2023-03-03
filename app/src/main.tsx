import React from 'react';
import ReactDOM from 'react-dom/client';

import { ApolloProvider } from '@apollo/client';
import { MantineProvider } from '@mantine/core';
import { RouterProvider } from 'react-router-dom';

import { AccessTokenProvider, ProfileProvider } from '~/components';
import { theme } from './theme';
import apolloClient from './apollo';
import router from './router';

const root = document.querySelector('#root')!;
ReactDOM.createRoot(root).render(
  <React.StrictMode>
    <ApolloProvider client={apolloClient}>
      <AccessTokenProvider>
        <ProfileProvider>
          <MantineProvider
            theme={theme}
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
