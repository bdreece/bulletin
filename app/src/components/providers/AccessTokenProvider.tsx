import type { AccessTokenResult } from '@generated/graphql/graphql';

import day from 'dayjs';
import { createContext, useContext, useEffect } from 'react';
import { useRefresh } from '~/hooks/auth';

export type AccessTokenContext = Omit<AccessTokenResult, '__typename'>;
export type AccessTokenProviderProps = React.PropsWithChildren;

export const ACCESS_TOKEN_KEY = 'BULLETIN_ACCESS_TOKEN';
export const ACCESS_TOKEN_EXPIRATION_KEY = 'BULLETIN_ACCESS_TOKEN_EXPIRATION';

const context = createContext<AccessTokenContext | undefined>(undefined);
export const useAccessToken = (): Partial<AccessTokenContext> =>
  useContext(context) ?? {};

const AccessTokenProvider: React.FC<AccessTokenProviderProps> = ({
  children,
}) => {
  const { data } = useRefresh();

  const accessToken = data?.refresh.result?.accessToken ?? '';
  const expiration = day(
    data?.refresh.result?.expiration ?? new Date(),
  ).toISOString();
  localStorage.setItem(ACCESS_TOKEN_KEY, accessToken);
  localStorage.setItem(ACCESS_TOKEN_EXPIRATION_KEY, expiration);

  useEffect(() => {
    console.debug({ accessToken, expiration });
  }, [accessToken, expiration]);

  return (
    <context.Provider value={data?.refresh?.result ?? undefined}>
      {children}
    </context.Provider>
  );
};

export default AccessTokenProvider;
