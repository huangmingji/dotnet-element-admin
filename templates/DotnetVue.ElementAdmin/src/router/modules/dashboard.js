import Layout from '../../layout/index'

const dashboardRouter = {
  path: '/dashboard',
  component: Layout,
  children: [
    {
      path: '',
      name: 'Dashboard',
      component: () => import('../../views/dashboard'),
      meta: {
        permissions: ['admin'],
        title: 'Dashboard',
        icon: 'el-icon-s-home'
      }
    }
  ]
}

export default dashboardRouter
