import { createContext, useContext } from 'react';
import { useToggle } from '../../hooks/toggle';

export type LayoutState = ReturnType<typeof useToggle>;

const context = createContext<LayoutState | undefined>(undefined);
export const useLayoutState = () => useContext(context)!;

export type LayoutStateProviderProps = React.PropsWithChildren;

const LayoutStateProvider: React.FC<LayoutStateProviderProps> = ({
  children,
}) => {
  const value = useToggle(false);
  return <context.Provider value={value}>{children}</context.Provider>;
};

export default LayoutStateProvider;
