import type { NavbarProps } from '@mantine/core';

import { useMemo } from 'react';
import { NavLink } from '~/components';
import { Navbar as MantineNavbar } from '@mantine/core';
import { useLayoutState, useProfile } from '~/components/providers';

const Navbar: React.FC = () => {
  const [opened] = useLayoutState();
  const { id, name } = useProfile();
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
      <MantineNavbar.Section grow>
        <NavLink
          to='/'
          label='Home'
        />
        {id && (
          <>
            <NavLink
              to='/documents'
              label='Documents'
            />
            <NavLink
              to='/calendars'
              label='Calendars'
            />
          </>
        )}
      </MantineNavbar.Section>
      <MantineNavbar.Section>
        {name ? (
          <NavLink
            to='/profile'
            label={name}
          />
        ) : (
          <>
            <NavLink
              to='/auth/login'
              label='Login'
            />
            <NavLink
              to='/auth/register'
              label='Sign Up'
            />
          </>
        )}
      </MantineNavbar.Section>
    </MantineNavbar>
  );
};

export default Navbar;
