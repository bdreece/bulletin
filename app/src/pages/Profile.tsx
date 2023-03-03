import type { ZodType } from 'zod';
import type { UpdateUserInput } from '@generated/graphql/graphql';

import z from 'zod';
import { Button, TextInput, Title } from '@mantine/core';
import { useForm, zodResolver } from '@mantine/form';

import { Card } from '~/components';
// import { useUpdateUser } from '~/hooks';
import { useProfile } from '~/components/providers';

const schema: ZodType<UpdateUserInput> = z.object({
  id: z.string(),
  firstName: z.string().optional(),
  lastName: z.string().optional(),
  email: z.string().optional(),
});

const Profile: React.FC = () => {
  const { id, name, email } = useProfile();
  // const [mutation, { loading }] = useUpdateUser();

  const [firstName, lastName] = name!.split(' ');
  const { getInputProps, ...form } = useForm<UpdateUserInput>({
    validate: zodResolver(schema),
    initialValues: {
      id: id!,
      email,
      firstName,
      lastName,
    },
  });

  const onSubmit = form.onSubmit(
    values => {}, // mutation({ variables: { input: values } }),
  );

  return (
    <Card>
      <Title>Profile</Title>
      <form onSubmit={onSubmit}>
        <TextInput
          mt='md'
          label='First Name'
          {...getInputProps('firstName')}
        />

        <TextInput
          mt='md'
          label='Last Name'
          {...getInputProps('lastName')}
        />

        <TextInput
          mt='md'
          type='email'
          label='Email Address'
          {...getInputProps('email')}
        />

        <Button type='submit'>{/*loading ? 'Loading' : 'Submit'*/}</Button>
      </form>
    </Card>
  );
};

export default Profile;
