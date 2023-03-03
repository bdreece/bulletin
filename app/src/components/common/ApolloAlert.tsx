import type { ApolloError } from '@apollo/client';
import { Alert } from '@mantine/core';

export type ApolloAlertProps = { error: ApolloError };

const ApolloAlert: React.FC<ApolloAlertProps> = ({ error }) => (
  <Alert title={error.name}>{error.message}</Alert>
);

export default ApolloAlert;
