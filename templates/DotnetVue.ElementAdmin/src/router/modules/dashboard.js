import Layout from '../../layout'

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
