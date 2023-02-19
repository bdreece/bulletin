import { useToggle as useMantineToggle } from '@mantine/hooks';

export const useToggle = (initialValue: boolean) =>
  useMantineToggle([initialValue, !initialValue]);
