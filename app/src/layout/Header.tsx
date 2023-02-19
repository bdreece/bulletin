import type { HeaderProps, MediaQueryProps, BurgerProps } from '@mantine/core';
import type { Styles } from 'types/styles';

import { useMemo } from 'react';
import {
  Header as MantineHeader,
  MediaQuery,
  Burger,
  Title,
  useMantineTheme,
} from '@mantine/core';
import { useLayoutState } from '../components/providers';

const Header: React.FC = () => {
  const theme = useMantineTheme();
  const [opened, toggle] = useLayoutState();
  const burger = useMemo(
    (): BurgerProps => ({
      opened,
      size: 'sm',
      mr: 'xl',
      onClick: _ => toggle(),
      color: theme.colors.gray[6],
    }),
    [opened, toggle],
  );

  return (
    <MantineHeader {...header}>
      <div style={styles.container}>
        <MediaQuery {...mediaQuery}>
          <Burger {...burger} />
        </MediaQuery>
        <Title>Application header</Title>
      </div>
    </MantineHeader>
  );
};

const header: Omit<HeaderProps, 'children'> = {
  p: 'md',
  height: {
    base: 50,
    md: 70,
  },
};

const mediaQuery: Omit<MediaQueryProps, 'children'> = {
  largerThan: 'sm',
  styles: {
    display: 'none',
  },
};

const styles: Styles = {
  container: {
    display: 'flex',
    alignItems: 'center',
    height: '100%',
  },
};

export default Header;
