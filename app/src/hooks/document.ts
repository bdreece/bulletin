import { CreateDocumentDocument } from '@generated/graphql/graphql';
import { createMutation } from './query';

export const useCreateDocument = createMutation(CreateDocumentDocument);
