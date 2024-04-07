import { createRouter, createWebHistory } from 'vue-router';

const routes = [
    {
        path: '/',
        name: 'Home',
        component: () => import('@/components/home/HomePage.vue'),
    },
    {
        path: '/about',
        name: 'About',
        component: () => import('@/components/home/AboutPage.vue'),
    },
    {
        path: '/privacy',
        name: 'Privacy',
        component: () => import('@/components/home/PrivacyPage.vue'),
    },
    {
        path: '/logs',
        name: 'Logs',
        component: () => import('@/components/appManagement/LogsViewer.vue'),
    },
];

const router = createRouter({
    history: createWebHistory(import.meta.env.BASE_URL),
    routes,
});

export default router;
