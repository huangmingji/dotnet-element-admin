import request from '../utils/request'

export function login (data) {
  return request({
    url: '/api/user/login',
    method: 'post',
    data: data
  })
}

export function getInfo () {
  return request({
    url: '/api/user',
    method: 'get'
  })
}

export function logout () {
  return request({
    url: '/api/user/logout',
    method: 'post'
  })
}
