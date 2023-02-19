import type { FooterProps } from '@mantine/core';

import { Footer as MantineFooter, Text } from '@mantine/core';

const Footer: React.FC = () => (
  <MantineFooter {...footer}>
    <Text>Copyright &copy; {year} Brian Reece</Text>
  </MantineFooter>
);

const year = new Date().getFullYear();
const footer: Omit<FooterProps, 'children'> = {
  p: 'md',
  height: 60,
};

export default Footer;
