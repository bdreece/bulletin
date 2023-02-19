import { ColProps, CardProps } from '@mantine/core';

import { Alert, Card, Grid, Loader } from '@mantine/core';
import { useProfile } from '../../components/providers/ProfileProvider';
import { useDocuments } from '../../graphql/document.graphql';

const Documents: React.FC = () => {
  const { id } = useProfile();
  const { data, loading, error } = useDocuments(
    {
      where: {
        id: { eq: id },
      },
    },
    {
      fetchPolicy: id ? 'cache-first' : 'standby',
    },
  );

  return (
    <Grid>
      {error ? (
        <Alert title={error.name}>{error.message}</Alert>
      ) : loading ? (
        <Loader />
      ) : (
        data?.documents?.map(doc => (
          <Grid.Col {...column}>
            <Card {...card}>{doc.name}</Card>
          </Grid.Col>
        ))
      )}
    </Grid>
  );
};

const column: Omit<ColProps, 'children'> = {
  span: 3,
};

const card: Omit<CardProps, 'children'> = {
  shadow: 'sm',
  p: 'lg',
  radius: 'md',
  withBorder: true,
};

export default Documents;
