import { lazy, Suspense } from 'react';
import { createBrowserRouter } from 'react-router-dom';

const Layout = lazy(() => import('~/layout'));
const Home = lazy(() => import('~/pages'));
const Profile = lazy(() => import('~/pages/Profile'));

const Login = lazy(() => import('~/pages/auth/Login'));
const Register = lazy(() => import('~/pages/auth/Register'));

const Calendars = lazy(() => import('~/pages/calendars'));
const Calendar = lazy(() => import('~/pages/calendars/Calendar'));

const DocumentLayout = lazy(() => import('~/pages/documents'));
const DocumentList = lazy(() => import('~/pages/documents/DocumentList'));
const DocumentDetail = lazy(() => import('~/pages/documents/DocumentDetail'));

const router = createBrowserRouter([
  {
    path: '/',
    element: <Layout />,
    children: [
      {
        index: true,
        element: (
          <Suspense>
            <Home />
          </Suspense>
        ),
      },
      {
        path: 'profile',
        element: (
          <Suspense>
            <Profile />
          </Suspense>
        ),
      },
      {
        path: 'calendars/',
        children: [
          {
            index: true,
            element: (
              <Suspense>
                <Calendars />
              </Suspense>
            ),
          },
          {
            path: ':calendarID',
            element: (
              <Suspense>
                <Calendar />
              </Suspense>
            ),
          },
        ],
      },
      {
        path: 'documents/',
        element: (
          <Suspense>
            <DocumentLayout />
          </Suspense>
        ),
        children: [
          {
            index: true,
            element: (
              <Suspense>
                <DocumentList />
              </Suspense>
            ),
          },
          {
            path: ':documentID',
            element: (
              <Suspense>
                <DocumentDetail />
              </Suspense>
            ),
          },
        ],
      },
      {
        path: 'auth/',
        children: [
          {
            path: 'login',
            element: (
              <Suspense>
                <Login />
              </Suspense>
            ),
          },
          {
            path: 'register',
            element: (
              <Suspense>
                <Register />
              </Suspense>
            ),
          },
        ],
      },
    ],
  },
]);

export default router;
