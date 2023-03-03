import type {
  Full_DirectoryFragment,
  Details_NamedEntityFragment,
} from '@generated/graphql/graphql';

import { map } from 'lodash/fp';
import { Container, Accordion, Button, Text, Alert } from '@mantine/core';

export type DirectoryTreeProps = {
  directories: Details_NamedEntityFragment[];
  directory: Full_DirectoryFragment | undefined;
  setDirectory: (newDirectory?: Details_NamedEntityFragment) => void;
};

const DirectoryTree: React.FC<DirectoryTreeProps> = ({
  directories,
  directory,
  setDirectory,
}) =>
  directory ? (
    <Container size='sm'>
      {directory.parentDirectory && (
        <Button onClick={() => setDirectory(directory.parentDirectory!)}>
          Back
        </Button>
      )}
      <Accordion defaultValue={directory.name}>
        {map(
          d => (
            <Accordion.Item value={d.name}>
              <Accordion.Control>{d.name}</Accordion.Control>
              <Accordion.Panel>
                <Text>{d.description}</Text>
                <Button onClick={() => setDirectory(d)}>Select</Button>
              </Accordion.Panel>
            </Accordion.Item>
          ),
          directories,
        )}
      </Accordion>
    </Container>
  ) : (
    <Alert color='gray'>No directories found</Alert>
  );

export default DirectoryTree;
