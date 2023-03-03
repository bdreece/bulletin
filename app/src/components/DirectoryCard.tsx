import type { Details_NamedEntityFragment } from '@generated/graphql/graphql';

import { Button } from '@mantine/core';
import { DetailCard } from './common';

export type DirectoryCardProps = Details_NamedEntityFragment & {
  setDirectory: (newDirectory: Details_NamedEntityFragment) => void;
};

const DirectoryCard: React.FC<DirectoryCardProps> = ({
  setDirectory,
  ...props
}) => (
  <DetailCard {...props}>
    <Button onClick={() => setDirectory(props)}>See Directory</Button>
  </DetailCard>
);
export default DirectoryCard;
