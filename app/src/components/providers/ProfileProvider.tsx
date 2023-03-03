import type { SelfResult } from '@generated/graphql/graphql';
import type { Impartial } from '~/types/partial';
import { useSelf } from '~/hooks/auth';
import { createContext, useContext, useMemo, useEffect } from 'react';
import { useAccessToken } from './AccessTokenProvider';

export type ProfileContext = Impartial<Omit<SelfResult, '__typename'>>;

const context = createContext<ProfileContext | undefined>(undefined);
export const useProfile = () => useContext(context)!;

export type ProfileProviderProps = React.PropsWithChildren;

const ProfileProvider: React.FC<ProfileProviderProps> = ({ children }) => {
  const { accessToken } = useAccessToken();
  const { data } = useSelf(undefined, {
    fetchPolicy: accessToken ? 'cache-first' : 'cache-only',
  });

  const profile = {
    id: data?.self?.id ?? null,
    name: data?.self?.name ?? null,
    email: data?.self?.email ?? null,
    roles: data?.self?.roles ?? [],
  };

  useEffect(() => data && console.debug({ profile }), [data]);

  return <context.Provider value={profile}>{children}</context.Provider>;
};

export default ProfileProvider;
