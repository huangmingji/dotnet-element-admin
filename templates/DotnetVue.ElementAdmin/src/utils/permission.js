import store from '@/store'

/**
 * @param {Array} value
 * @returns {Boolean}
 * @example see @/views/permission/directive.vue
 */
export default function checkPermission (value) {
  if (value && value instanceof Array && value.length > 0) {
    const roles = store.getters && store.getters.permissions
    const permissions = value

    const hasPermission = roles.some(permission => {
      return permissions.includes(permission)
    })

    if (!hasPermission) {
      return false
    }
    return true
  } else {
    console.error(`need permissions! Like v-permission="['admin','editor']"`)
    return false
  }
}
