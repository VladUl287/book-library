import { authModule } from './../store/modules/auth';
import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router'
import MainView from '@/views/MainView.vue'
import BookmarkView from '@/views/BookmarkView.vue'

const routes: Array<RouteRecordRaw> = [
  {
    path: '/',
    name: 'main',
    component: MainView,
    meta: { requiresAuth: true }
  },
  {
    path: '/bookmarks',
    name: 'bookmark',
    component: BookmarkView,
    meta: { requiresAuth: true }
  },
  {
    path: '/auth',
    name: 'auth',
    component: () => import('../views/AuthView.vue'),
    meta: { guest: true }
  }
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

router.beforeEach((to, _, next) => {
  console.log(authModule);
  
  if (to.matched.some(record => record.meta.requiresAuth)) {
    if (authModule.isAuthenticated) {
      next()
      return
    }
    next('/auth')
  } else {
    next()
  }
})

router.beforeEach((to, _, next) => {
  if (to.matched.some((record) => record.meta.guest)) {
    if (authModule.isAuthenticated) {
      next("/");
      return;
    }
    next();
  } else {
    next();
  }
});

export default router
