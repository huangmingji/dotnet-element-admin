import Vue from 'vue'
import Router from 'vue-router'
import dashboardRouter from './modules/dashboard'
import demoRouter from './modules/demo'

Vue.use(Router)

const originalPush = Router.prototype.push

Router.prototype.push = function push (location) {
  return originalPush.call(this, location).catch(err => console.log(err))
}

/**
 * constantRoutes
 * a base page that does not have permission requirements
 * all roles can be accessed
 */
export const constantRoutes = [
  {
    path: '/account/login',
    component: () => import('../views/login/index'),
    hidden: true
  },
  {
    path: '/',
    redirect: '/dashboard',
    hidden: true
  }
]

/**
 * asyncRoutes
 * the routes that need to be dynamically loaded based on user roles
 */
export const asyncRoutes = [
  dashboardRouter,
  demoRouter
]

const createRouter = () => new Router({
  mode: 'history', // hash/history
  scrollBehavior: () => ({ y: 0 }),
  routes: constantRoutes
})

const router = createRouter()

export function resetRouter () {
  const newRouter = createRouter()
  router.matcher = newRouter.matcher // reset router
}
export default router
