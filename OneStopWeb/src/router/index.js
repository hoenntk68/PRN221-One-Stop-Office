import { createRouter, createWebHistory } from 'vue-router'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: () => import('../views/HomePage.vue')
    },
    {
      path: '/login',
      name: 'login',
      component: () => import('../views/Login.vue')
    },
    {
      path: '/about',
      name: 'about',
      component: () => import('../views/AboutView.vue')
    },
    {
      path: '/request',
      name: 'request',
      component: () => import('../views/AboutView.vue')
    },
    {
      path: '/request-list',
      name: 'requestList',
      component: () => import('../views/RequestListView.vue')
    },
    {
      path: '/request-submit',
      name: 'submitRequest',
      component: () => import('../views/SubmitRequestView.vue')
    }
  ]
})

export default router
