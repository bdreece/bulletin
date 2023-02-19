import type { NavbarProps } from '@mantine/core';

import { useMemo } from 'react';
import { Navbar as MantineNavbar, Text } from '@mantine/core';
import { useLayoutState } from '../components/providers';

const Navbar: React.FC = () => {
  const [opened] = useLayoutState();
  const navbar = useMemo(
    (): Omit<NavbarProps, 'children'> => ({
      p: 'md',
      hidden: !opened,
      hiddenBreakpoint: 'sm',
      width: {
        sm: 200,
        lg: 300,
      },
    }),
    [opened],
  );

  return (
    <MantineNavbar {...navbar}>
      <Text>Navbar</Text>
    </MantineNavbar>
  );
};

export default Navbar;
