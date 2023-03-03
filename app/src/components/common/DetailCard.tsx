import type { Details_NamedEntityFragment } from '@generated/graphql/graphql';

import { Title, Text } from '@mantine/core';
import { Link } from 'react-router-dom';

import Card from './Card';

export type DetailCardProps = Details_NamedEntityFragment &
  React.PropsWithChildren;

const DetailCard: React.FC<DetailCardProps> = ({
  name,
  description,
  children,
}) => {
  return (
    <Card>
      <Title>{name}</Title>
      <Text>{description}</Text>
      {children}
    </Card>
  );
};

export default DetailCard;
