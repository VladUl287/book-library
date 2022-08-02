import { authModule } from './../store/modules/auth';
import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router'
import BooksView from '@/views/BooksView.vue'
import BookView from '@/views/BookView.vue'
import ProfileView from '@/views/ProfileView.vue'
import ReadListView from '@/views/ReadListView.vue'
import BookmarkView from '@/views/BookmarkView.vue'
import NoveltiesView from '@/views/NoveltiesView.vue'
import CollectionView from '@/views/CollectionView.vue'
import CollectionsView from '@/views/CollectionsView.vue'
import RecommendationsView from '@/views/RecommendationsView.vue'

const routes: Array<RouteRecordRaw> = [
  {
    path: '/',
    name: 'main',
    component: BooksView
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
    path: '/novelties',
    name: 'novelties',
    component: NoveltiesView,
    meta: { requiresAuth: true }
  },
  {
    path: '/collections',
    name: 'collections',
    component: CollectionsView,
    meta: { requiresAuth: true }
  },
  {
    path: '/collection/:collectionId',
    name: 'collection',
    component: CollectionView,
    meta: { requiresAuth: true }
  },
  {
    path: '/read-list',
    name: 'read-list',
    component: ReadListView,
    meta: { requiresAuth: true }
  },
  {
    path: '/profile',
    name: 'profile',
    component: ProfileView,
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

// router.beforeEach((to, _, next) => {  
  // if (to.matched.some(record => record.meta.requiresAuth)) {
  //   if (authModule.isAuthenticated) {
  //     next()
  //     return
  //   }
  //   next('/auth')
  // } else {
  //   next()
  // }
// })

// router.beforeEach((to, _, next) => {
  // if (to.matched.some((record) => record.meta.guest)) {
  //   if (authModule.isAuthenticated) {
  //     next("/");
  //     return;
  //   }
  //   next();
  // } else {
  //   next();
  // }
// });

export default router
