import { Title, Text } from '@mantine/core';
import { Card } from '~/components';

const Calendars: React.FC = () => (
  <Card>
    <Title order={3}>Calendars</Title>
    <Text>
      Lorem ipsum dolor sit amet consectetur adipisicing elit. Facilis atque
      placeat magnam iste tenetur commodi sunt iure labore! Nemo, obcaecati?
      Quod at temporibus, vel eveniet adipisci error libero ex ratione!
    </Text>
  </Card>
);

export { default as Calendar } from './Calendar';
export default Calendars;
