import Layout from '../../layout/index'

const demoRouter = {
  path: '/demo',
  component: Layout,
  meta: {
    title: 'demo',
    icon: 'el-icon-menu'
  },
  children: [
    {
      path: 'editor',
      name: 'Editor',
      component: () => import('../../components/RichTextEditor/demo'),
      meta: {
        title: 'Editor',
        permissions: ['admin'],
        icon: 'el-icon-edit'
      }
    },
    {
      path: 'upload',
      name: 'Upload',
      component: () => import('../../components/Upload/demo'),
      meta: {
        title: 'Upload',
        permissions: ['admin'],
        icon: 'el-icon-upload'
      }
    },
    {
      path: 'echarts',
      name: 'Echarts',
      component: () => import('../../components/Echarts/demo'),
      meta: {
        title: 'Echarts',
        permissions: ['admin'],
        icon: 'el-icon-pie-chart'
      }
    }
  ]
}

export default demoRouter
