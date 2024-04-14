import { createRouter, createWebHistory } from 'vue-router';

const routes = [
    {
        path: '/',
        name: 'HomePage',
        component: () => import('@/components/home/HomePage.vue'),
    },
    {
        path: '/about',
        name: 'AboutPage',
        component: () => import('@/components/home/AboutPage.vue'),
    },
    {
        path: '/privacy',
        name: 'PrivacyPage',
        component: () => import('@/components/home/PrivacyPage.vue'),
    },
    {
        path: '/logs',
        name: 'LogsViewer',
        component: () => import('@/components/appManagement/LogsViewer.vue'),
    },
    {
        path: '/categories',
        name: 'IndexCategories',
        component: () => import('@/components/categories/IndexCategories.vue'),
    },
    {
        path: '/products',
        name: 'IndexProducts',
        component: () => import('@/components/products/IndexProducts.vue'),
    },


    {
        path: '/create-category',
        name: 'CreateCategory',
        component: () => import('@/components/categories/CreateCategory.vue'),
    },
    {
        path: '/category-details/:id',
        name: 'CategoryDetails',
        component: () => import('@/components/categories/CategoryDetails.vue'),
    },
    {
        path: '/edit-category/:id',
        name: 'EditCategory',
        component: () => import('@/components/categories/EditCategory.vue'),
    },

    
    {
        path: '/create-product',
        name: 'CreateProduct',
        component: () => import('@/components/products/CreateProduct.vue'),
    },
    {
        path: '/category-product/:id',
        name: 'ProductDetails',
        component: () => import('@/components/products/ProductDetails.vue'),
    },
    {
        path: '/edit-product/:id',
        name: 'EditProduct',
        component: () => import('@/components/products/EditProduct.vue'),
    },
    {
        path: '/category-products/:id',
        name: 'CategoryProducts',
        component: () => import('@/components/products/CategoryProducts.vue'),
    },
];

const router = createRouter({
    history: createWebHistory(import.meta.env.BASE_URL),
    routes,
});

export default router;
