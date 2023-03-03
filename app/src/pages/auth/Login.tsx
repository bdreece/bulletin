import type { ZodType } from 'zod';
import type { LoginInput } from '@generated/graphql/graphql';

import z from 'zod';
import { Button, Title, TextInput, Alert } from '@mantine/core';
import { useForm, zodResolver } from '@mantine/form';
import { Card } from '~/components';
import { useLogin } from '~/hooks/auth';

const schema: ZodType<LoginInput> = z.object({
  email: z.string(),
  password: z.string(),
});

const Login: React.FC = () => {
  const [mutation, { data, loading }] = useLogin();
  const { getInputProps, ...form } = useForm<LoginInput>({
    validate: zodResolver(schema),
    initialValues: {
      email: '',
      password: '',
    },
  });

  const onSubmit = form.onSubmit(values =>
    mutation({ variables: { input: values } }).then(_ =>
      console.log('Logged in!'),
    ),
  );

  const errors = data?.login.errors?.map(e => e.message).join('\n');

  return (
    <Card>
      <Title order={3}>Login</Title>

      {errors && (
        <Alert
          mt='md'
          color='red'
          title='An error occurred:'
        >
          {errors}
        </Alert>
      )}

      <form onSubmit={onSubmit}>
        <TextInput
          withAsterisk
          mt='md'
          label='Email'
          placeholder='johndoe@business.net'
          {...getInputProps('email')}
        />

        <TextInput
          withAsterisk
          mt='md'
          label='Password'
          type='password'
          placeholder='password123'
          {...getInputProps('password')}
        />

        <Button
          type='submit'
          mt='md'
        >
          {loading ? 'Loading' : 'Submit'}
        </Button>
      </form>
    </Card>
  );
};

export default Login;
