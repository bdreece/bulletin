import type { CardProps as MantineCardProps } from '@mantine/core';

import { Card as MantineCard } from '@mantine/core';

export type CardProps = React.PropsWithChildren;

const Card: React.FC<CardProps> = ({ children }) => (
  <MantineCard {...card}>{children}</MantineCard>
);

const card: Omit<MantineCardProps, 'children'> = {
  withBorder: true,
  shadow: 'sm',
  radius: 'md',
  p: 'lg',
};

export default Card;
