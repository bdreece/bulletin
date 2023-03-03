import type { ZodType } from 'zod';
import type { RegisterInput } from '@generated/graphql/graphql';

import z from 'zod';
import { Button, TextInput, Title } from '@mantine/core';
import { useForm, zodResolver } from '@mantine/form';
import { useRegister } from '~/hooks/auth';
import { ApolloAlert, Card } from '~/components';

const letterRegex = /^.*([a-zA-Z]+).*$/;
const numberRegex = /^.*([0-9]+).*$/;
const specialCharacterRegex = /^.*([!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?]+).*$/;

const schema: ZodType<RegisterInput> = z.object({
  firstName: z.string().min(1, 'First name is required'),
  lastName: z.string().min(1, 'Last name is required'),
  email: z.string().email(),
  password: z
    .string()
    .min(8, 'Password must be at least 8 characters')
    .regex(letterRegex, 'Password must contain letters')
    .regex(numberRegex, 'Password must contain numbers')
    .regex(specialCharacterRegex, 'Password must contain special characters'),
});

const Register: React.FC = () => {
  const [mutation, { loading, error }] = useRegister();
  const { getInputProps, ...form } = useForm<RegisterInput>({
    validate: zodResolver(schema),
    validateInputOnBlur: true,
    initialValues: {
      firstName: '',
      lastName: '',
      email: '',
      password: '',
    },
  });

  const onSubmit = form.onSubmit(values =>
    mutation({ variables: { input: values } }).then(_ =>
      console.log('Registered successfully!'),
    ),
  );

  return (
    <Card>
      <Title order={3}>Register</Title>

      {error && <ApolloAlert error={error} />}

      <form onSubmit={onSubmit}>
        <TextInput
          withAsterisk
          mt='md'
          label='First Name'
          placeholder='John'
          {...getInputProps('firstName')}
        />

        <TextInput
          withAsterisk
          mt='md'
          label='Last Name'
          placeholder='Doe'
          {...getInputProps('lastName')}
        />

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

export default Register;
