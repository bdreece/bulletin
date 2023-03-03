import { Group, Title } from '@mantine/core';

export type ContextBarProps = React.PropsWithChildren & {
  title: React.ReactNode;
};

const ContextBar: React.FC<ContextBarProps> = ({ children, title }) => (
  <Group position='apart'>
    <Title order={3}>{title}</Title>
    <Group position='right'>{children}</Group>
  </Group>
);

export default ContextBar;
