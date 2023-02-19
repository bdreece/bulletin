import { useLogin } from '~/graphql/auth.graphql';

const Login: React.FC = () => {
  const [mutation, { data, loading, error }] = useLogin();

  return <article></article>;
};

export default Login;
