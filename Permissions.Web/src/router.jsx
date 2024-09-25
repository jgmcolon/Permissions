import { lazy, Suspense } from 'react';
import { Outlet, Navigate, useRoutes } from 'react-router-dom';

import AdminLayout from './layouts/admin/Index';
import {Box, LinearProgress, linearProgressClasses} from "@mui/material";

// ----------------------------------------------------------------------

export const RequestPage = lazy(() => import('./pages/permission/requestPage'));
export const ListPage = lazy(() => import('./pages/permission/listPage.jsx'));
export const ModifyPage = lazy(() => import('./pages/permission/modifyPage.jsx'));
export const HomePage = lazy(() => import('./pages/homePage.jsx'));
export const Page404 = lazy(() => import('./pages/notFoundPage.jsx'));

export const PermissionTypePage = lazy(() => import('./pages/permissionType/permissionTypePage.jsx'));
// ----------------------------------------------------------------------

const renderFallback = (
    <Box display="flex" alignItems="center" justifyContent="center" flex="1 1 auto">
        <LinearProgress
            sx={{
                width: 1,
                maxWidth: 320,
                [`& .${linearProgressClasses.bar}`]: { bgcolor: 'text.primary' },
            }}
        />
    </Box>
);

export default function Router() {
    return useRoutes([
        {
            element: <AdminLayout/>,
            children: [
                {element: <HomePage/>, index: true},
                {path: 'request', element: <RequestPage/>},
                {path: 'modify/:Id', element: <ModifyPage/>},
                {path: 'list', element: <ListPage/>},
                {path: 'permission-type', element: <PermissionTypePage/>},
            ],
        },

        {
            path: 'page-not-found',
            element: <Page404/>,
        },
        {
            path: '*',
            element: <Navigate to="/page-not-found" replace/>,
        },
    ]);

}
