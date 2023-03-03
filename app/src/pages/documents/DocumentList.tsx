import { Alert, ColProps } from '@mantine/core';
import type { Full_DirectoryFragment } from '@generated/graphql/graphql';
import type { DocumentLayoutState } from '.';

import { useOutletContext } from 'react-router-dom';
import { Grid } from '@mantine/core';
import { DocumentCard } from '~/components';

const DocumentList: React.FC = () => {
  const { directory } = useOutletContext<DocumentLayoutState>();
  const documents = directory?.documents;

  return documents ? (
    <Grid>
      {documents.map((d, i) => (
        <Grid.Col
          key={i}
          {...column}
        >
          <DocumentCard {...d} />;
        </Grid.Col>
      ))}
    </Grid>
  ) : (
    <Alert color='gray'>
      {directory ? 'This directory is empty' : 'No directory selected'}
    </Alert>
  );
};

const column: Omit<ColProps, 'children'> = {
  span: 3,
};

export default DocumentList;
