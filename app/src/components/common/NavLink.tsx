import type { NavLinkProps as RouterNavLinkProps } from 'react-router-dom';
import type { NavLinkProps as MantineNavLinkProps } from '@mantine/core';

import { NavLink as RouterNavLink } from 'react-router-dom';
import { NavLink as MantineNavLink } from '@mantine/core';

export type NavLinkProps = React.PropsWithChildren &
  Pick<RouterNavLinkProps, 'to'> &
  Omit<MantineNavLinkProps, 'active'>;

const NavLink: React.FC<NavLinkProps> = ({ to, children, ...props }) => (
  <RouterNavLink to={to}>
    {({ isActive }) => (
      <MantineNavLink
        active={isActive}
        {...props}
      >
        {children}
      </MantineNavLink>
    )}
  </RouterNavLink>
);

export default NavLink;
