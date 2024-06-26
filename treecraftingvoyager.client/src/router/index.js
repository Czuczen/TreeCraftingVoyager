import { createRouter, createWebHistory } from 'vue-router';
import store from '@/store';

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
        meta: {
            requiresAuth: true,
            isAdmin: true
        }
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
        meta: {
            requiresAuth: true,
        }
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
        meta: {
            requiresAuth: true,
        }
    },

    
    {
        path: '/create-product',
        name: 'CreateProduct',
        component: () => import('@/components/products/CreateProduct.vue'),
        meta: {
            requiresAuth: true,
        }
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
        meta: {
            requiresAuth: true,
        }
    },
    {
        path: '/category-products/:id',
        name: 'CategoryProducts',
        component: () => import('@/components/products/CategoryProducts.vue'),
    },




    {
        path: '/login',
        name: 'Login',
        component: () => import('@/components/auth/Login.vue'),
    },
    {
        path: '/register',
        name: 'Register',
        component: () => import('@/components/auth/Register.vue'),
    },
    //{
    //    path: '/admin',
    //    name: 'admin',
    //    component: () => import('@/components/auth/admin.vue'),
    //    meta: {
    //        requiresAuth: true,
    //        isAdmin: true
    //    }
    //}
];

const router = createRouter({
    history: createWebHistory(import.meta.env.BASE_URL),
    routes,
});

//router.beforeEach((to, from, next) => {
//    if (to.matched.some(record => record.meta.requiresAuth)) {
//        if (!store.getters.isLoggedIn) {
//            next('/login');
//        } else {
//            next();
//        }
//    } else {
//        next();
//    }
//});

export default router;
