import { authModule } from './../store/modules/auth';
import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router'
import BooksView from '@/views/BooksView.vue'
import BookView from '@/views/BookView.vue'
import BookmarkView from '@/views/BookmarkView.vue'
import RecommendationsView from '@/views/RecommendationsView.vue'

const routes: Array<RouteRecordRaw> = [
  {
    path: '/',
    name: 'main',
    component: BooksView,
    meta: { requiresAuth: true }
  },
  {
    path: '/bookmarks',
    name: 'bookmark',
    component: BookmarkView,
    meta: { requiresAuth: true }
  },
  {
    path: '/book/:bookId',
    name: 'book',
    component: BookView,
    meta: { requiresAuth: true }
  },
  {
    path: '/recommendations',
    name: 'recommendations',
    component: RecommendationsView,
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
