import { Title, Text } from '@mantine/core';
import { Card } from '~/components';

const Home: React.FC = () => (
  <Card>
    <Title order={3}>foobar</Title>
    <Text mt={10}>
      Lorem ipsum dolor sit amet consectetur adipisicing elit. Velit, dolores
      sequi! Consectetur tempora dolore laudantium alias facilis sequi sunt,
      modi esse non quaerat quis nemo beatae expedita quidem cumque ut?
    </Text>
  </Card>
);

export { default as Profile } from './Profile';
export default Home;
