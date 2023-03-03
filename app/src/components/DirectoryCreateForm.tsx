import type { ZodType } from 'zod';
import type { CreateDirectoryInput } from '@generated/graphql/graphql';

import z from 'zod';
import { TextInput, Button, Select } from '@mantine/core';
import { useForm, zodResolver } from '@mantine/form';
import { useCreateDirectory } from '~/hooks/directory';
import { useProfile } from './providers';
import { ApolloAlert } from './common';

const schema: ZodType<CreateDirectoryInput> = z.object({
  name: z.string(),
  userID: z.string().uuid(),
  description: z.string().optional(),
  parentDirectoryID: z.string().uuid().optional(),
});

const DirectoryCreateForm: React.FC = () => {
  const { id } = useProfile();
  const [mutation, { loading, error }] = useCreateDirectory();
  const { getInputProps, ...form } = useForm<CreateDirectoryInput>({
    validate: zodResolver(schema),
    validateInputOnBlur: true,
    initialValues: {
      name: '',
      userID: id!,
      parentDirectoryID: undefined,
      description: undefined,
    },
  });

  const onSubmit: React.FormEventHandler = e => {
    e.preventDefault();
    form.onSubmit(values =>
      mutation({ variables: { input: values } }).then(_ =>
        console.log('Directory created!'),
      ),
    );
  };

  return (
    <>
      {error && <ApolloAlert error={error} />}

      <form onSubmit={onSubmit}>
        <TextInput
          withAsterisk
          mt='md'
          label='Name'
          placeholder='New Directory'
          {...getInputProps('name')}
        />

        <TextInput
          mt='md'
          label='Description'
          {...getInputProps('description')}
        />

        <Select
          mt='md'
          label='Parent Directory'
          placeholder='see directories'
          data={[]}
        />

        <Button
          type='submit'
          mt='md'
        >
          {loading ? 'Loading' : 'Submit'}
        </Button>
      </form>
    </>
  );
};

export default DirectoryCreateForm;
