import axios from 'axios'
import { Message } from 'element-ui'
import store from '@/store'

// 创建axios实例
const service = axios.create({
  // baseURL: 'http://localhost:5000', // process.env.VUE_APP_BASE_API, // api的base_url
  timeout: 60000 // 请求超时时间
})

// request拦截器
service.interceptors.request.use(
  config => {
    config.headers['Cache-Control'] = 'no-cache'
    if (store.getters.token) {
      config.headers['Authorization'] = 'Bearer ' + store.getters.token // 让每个请求携带自定义token 请根据实际情况自行修改
    }
    return config
  },
  error => {
    // Do something with request error
    console.log(error) // for debug
    Promise.reject(error)
  }
)

// respone拦截器
service.interceptors.response.use(
  response => {
    return response.data
  },
  error => {
    console.log('err' + error) // for debug
    if (error.response && error.response.status === 401) {
      store.dispatch('user/resetToken').then(() => {
        location.reload() // 为了重新实例化vue-router对象 避免bug
      })
    } else {
      Message({
        message: error.message,
        type: 'error',
        duration: 5 * 1000
      })
    }
    return Promise.reject(error)
  }
)

export default service
