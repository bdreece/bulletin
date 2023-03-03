import type {
  Full_DirectoryFragment,
  Details_NamedEntityFragment,
} from '@generated/graphql/graphql';
import { Outlet } from 'react-router-dom';
import { Button, Drawer, Group, Loader, Text } from '@mantine/core';

import { filter } from 'lodash/fp';
import { useState, useEffect } from 'react';
import { ApolloAlert, Card, ContextBar, DirectoryTree } from '~/components';
import { useDirectories } from '~/hooks/directory';
import { useProfile } from '~/components/providers/ProfileProvider';
import { DocumentCreateForm, DirectoryCreateForm } from '~/components';

export type DocumentLayoutState = {
  directory: Full_DirectoryFragment | undefined;
  setDirectory: (newDirectory?: Details_NamedEntityFragment) => void;
};

type DrawerState = {
  opened: boolean;
  form?: 'document' | 'directory' | undefined;
};

const DocumentLayout: React.FC = () => {
  const { id } = useProfile();
  const [{ opened, form }, setState] = useState<DrawerState>({ opened: false });
  const toggle = () => setState(s => ({ ...s, opened: !opened }));
  const toggleCreateDirectory = () =>
    setState(s => ({
      opened: !opened,
      form: !s.form ? 'directory' : undefined,
    }));

  const toggleCreateDocument = () =>
    setState(s => ({
      opened: !opened,
      form: !s.form ? 'document' : undefined,
    }));

  const { data, loading, error } = useDirectories({
    userID: id!,
  });

  const directories = data?.directories ?? [];
  const [directory, _setState] = useState<Full_DirectoryFragment>();

  const state: DocumentLayoutState = {
    directory,
    setDirectory: newDirectory =>
      _setState(directories.find(d => d.id == newDirectory?.id)),
  };

  const directoryTree = {
    ...state,
    directories: directory
      ? filter(d => d.parentDirectory!.id === directory.id, directories)
      : directories,
  };

  useEffect(
    () => console.debug({ directories: directoryTree.directories }),
    [directoryTree],
  );

  return (
    <>
      <Card>
        <ContextBar title='Documents'>
          {directory && (
            <Button onClick={_ => toggleCreateDocument()}>New Document</Button>
          )}
          <Button onClick={_ => toggleCreateDirectory()}>New Directory</Button>
        </ContextBar>
        {loading ? (
          <Loader size='lg' />
        ) : error ? (
          <ApolloAlert error={error} />
        ) : (
          <Group>
            <DirectoryTree {...directoryTree} />
            <Outlet context={state} />
          </Group>
        )}
      </Card>
      <Drawer
        opened={opened}
        onClose={toggle}
        position='right'
      >
        {form === 'directory' ? (
          <DirectoryCreateForm />
        ) : form === 'document' ? (
          <DocumentCreateForm />
        ) : (
          'No form loaded!'
        )}
      </Drawer>
    </>
  );
};

export { default as Document } from './DocumentDetail';
export default DocumentLayout;
