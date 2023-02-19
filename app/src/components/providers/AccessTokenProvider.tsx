import type { AccessTokenResult } from '@generated/graphql/graphql';

import day from 'dayjs';
import { createContext, useContext, useEffect } from 'react';
import { useRefresh } from '~/graphql/auth.graphql';

export type AccessTokenContext = Omit<AccessTokenResult, '__typename'>;

const context = createContext<AccessTokenContext | undefined>(undefined);
export const useAccessToken = (): Partial<AccessTokenContext> =>
  useContext(context) ?? {};

export type AccessTokenProviderProps = React.PropsWithChildren;

const AccessTokenProvider: React.FC<AccessTokenProviderProps> = ({
  children,
}) => {
  const { data, error } = useRefresh(undefined, {
    nextFetchPolicy: () => {
      const expiration = data?.refresh?.result?.expiration;
      return expiration && day(expiration).isBefore(day())
        ? 'cache-only'
        : 'network-only';
    },
  });

  useEffect(
    () =>
      error && console.error('Error refreshing access token: ', error.message),
    [error],
  );

  useEffect(() => {
    const accessToken = data?.refresh?.result?.accessToken;
    accessToken && sessionStorage.setItem('BULLETIN_ACCESS_TOKEN', accessToken);
  }, [data]);

  return (
    <context.Provider value={data?.refresh?.result ?? undefined}>
      {children}
    </context.Provider>
  );
};

export default AccessTokenProvider;
