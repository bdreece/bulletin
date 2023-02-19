import { graphql } from '@generated/graphql';
import { createQuery } from '~/hooks/query';

export const useDocuments = createQuery(
  graphql(`
    query GetDocuments($where: DocumentFilterInput) {
      documents(where: $where) {
        id
        name
      }
    }
  `),
);

export const useDocument = createQuery(
  graphql(`
    query GetDocument($where: DocumentFilterInput!) {
      documents(where: $where) {
        id
        name
        filename
        directory {
          id
          name
        }
      }
    }
  `),
);
