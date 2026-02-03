import { createRouter, createWebHistory } from 'vue-router';

const routes = [
    {
        path: '/',
        name: 'Home',
        component: () => import('../views/HomePage.vue')
    },
    {
        path: '/movie/:id',
        name: 'MovieDetails',
        component: () => import('../views/MovieDetailsPage.vue')
    }
];

const router = createRouter({
    history: createWebHistory(),
    routes
});

export default router;
