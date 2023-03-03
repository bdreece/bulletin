import {
  CreateDirectoryDocument,
  GetDirectoriesDocument,
} from '@generated/graphql/graphql';
import { createMutation, createQuery } from './query';

export const useDirectories = createQuery(GetDirectoriesDocument);
export const useCreateDirectory = createMutation(CreateDirectoryDocument);
