import type { Details_NamedEntityFragment } from '@generated/graphql/graphql';
import { Link } from 'react-router-dom';
import { DetailCard } from './common';

export type DocumentCardProps = Details_NamedEntityFragment;
const DocumentCard: React.FC<DocumentCardProps> = props => {
  return (
    <DetailCard {...props}>
      <Link to={`/documents/${props.id}`}>See Document</Link>
    </DetailCard>
  );
};

export default DocumentCard;
