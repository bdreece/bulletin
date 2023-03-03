import type { ZodType } from 'zod';
import type { CreateDocumentInput } from '@generated/graphql/graphql';

import z from 'zod';
import { TextInput, Button, Select } from '@mantine/core';
import { useForm, zodResolver } from '@mantine/form';
import { useCreateDocument } from '~/hooks/document';
import { useProfile } from './providers';
import { ApolloAlert } from './common';

const schema: ZodType<CreateDocumentInput> = z.object({
  userID: z.string().uuid(),
  directoryID: z.string().uuid(),
  filename: z.string(),
  name: z.string(),
  description: z.string().optional(),
});

const DocumentCreateForm: React.FC = () => {
  const { id } = useProfile();
  const [mutation, { loading, error }] = useCreateDocument();
  const { getInputProps, ...form } = useForm<CreateDocumentInput>({
    validate: zodResolver(schema),
    validateInputOnBlur: true,
    initialValues: {
      userID: id!,
      directoryID: '',
      filename: '',
      name: '',
      description: undefined,
    },
  });

  const onSubmit = form.onSubmit(values =>
    mutation({ variables: { input: values } }).then(_ =>
      console.log('Document created!'),
    ),
  );

  return (
    <>
      {error && <ApolloAlert error={error} />}

      <form onSubmit={onSubmit}>
        <TextInput
          withAsterisk
          mt='md'
          label='Name'
          placeholder='New Document'
          {...getInputProps('name')}
        />

        <TextInput
          mt='md'
          label='Description'
          {...getInputProps('description')}
        />

        <Select
          withAsterisk
          mt='md'
          label='Directory'
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

export default DocumentCreateForm;
