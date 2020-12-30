import { login, logout, getInfo } from '../../api/user'
import { getToken, setToken, removeToken } from '../../utils/auth'
import { resetRouter } from '../../router'

const state = {
  token: getToken(),
  userId: '0',
  name: 'washon',
  avatar: 'static/img/portrait.jpg',
  permissions: []
}

const mutations = {
  SET_TOKEN: (state, token) => {
    state.token = token
  },
  SET_USERID: (state, userid) => {
    state.userId = userid
  },
  SET_NAME: (state, name) => {
    state.name = name
  },
  SET_AVATAR: (state, avatar) => {
    state.avatar = avatar
  },
  SET_PERMISSIONS: (state, permissions) => {
    state.permissions = permissions
  }
}

const actions = {
  // user login
  login ({ commit }, userInfo) {
    const { userName, password } = userInfo
    return new Promise((resolve, reject) => {
      login({ userName: userName.trim(), password: password }).then(response => {
        if (response.success) {
          commit('SET_TOKEN', response.token)
          setToken(response.token)
        }
        resolve(response)
      }).catch(error => {
        reject(error)
      })
    })
  },

  // get user info
  getInfo ({ commit, state }) {
    return new Promise((resolve, reject) => {
      getInfo(state.token).then(response => {
        commit('SET_USERID', response.id)
        commit('SET_NAME', response.name)
        commit('SET_AVATAR', response.headIcon)
        commit('SET_PERMISSIONS', response.permissions)
        resolve(response)
      }).catch(error => {
        reject(error)
      })
    })
  },

  // user logout
  logout ({ commit, state }) {
    return new Promise((resolve, reject) => {
      logout(state.token).then(() => {
        commit('SET_TOKEN', '')
        commit('SET_USERID', '')
        commit('SET_NAME', '')
        commit('SET_AVATAR', '')
        removeToken()
        resetRouter()
        resolve()
      }).catch(error => {
        reject(error)
      })
    })
  },

  // remove token
  resetToken ({ commit }) {
    return new Promise(resolve => {
      commit('SET_TOKEN', '')
      commit('SET_USERID', '')
      commit('SET_NAME', '')
      commit('SET_AVATAR', '')
      removeToken()
      resolve()
    })
  }
}

export default {
  namespaced: true,
  state,
  mutations,
  actions
}
