<template>
  <el-header>
    <router-link class="logo" to="/dashboard">
        <img v-if="logo" :src="logo" class="sidebar-logo">
        <span>{{ title }}</span>
    </router-link>
    <navbar />
  </el-header>
</template>

<script>
import defaultSettings from '../../../settings'
import Navbar from './Navbar'

export default {
  name: 'AppHeader',
  components: {
    Navbar
  },
  props: {
    isCollapse: {
      type: Boolean,
      default: false
    },
    collapseHandle: {
      type: Function,
      default: function () { }
    }
  },
  data () {
    return {
      title: defaultSettings.title,
      logo: 'static/img/logo.png'
    }
  }
}
</script>
