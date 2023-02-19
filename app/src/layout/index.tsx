import type { AppShellProps } from '@mantine/core';

import { AppShell } from '@mantine/core';
import { LayoutStateProvider } from '../components';
import { Outlet } from 'react-router-dom';
import Footer from './Footer';
import Header from './Header';
import Navbar from './Navbar';

const Layout: React.FC = () => {
  const appShell: Omit<AppShellProps, 'children'> = {
    header: <Header />,
    footer: <Footer />,
    navbar: <Navbar />,
    navbarOffsetBreakpoint: 'sm',
    sx: theme => ({
      main: {
        background:
          theme.colorScheme === 'dark'
            ? theme.colors.dark[8]
            : theme.colors.gray[0],
      },
    }),
  };

  return (
    <LayoutStateProvider>
      <AppShell {...appShell}>
        <Outlet />
      </AppShell>
    </LayoutStateProvider>
  );
};

export default Layout;
