<template>
  <el-aside :style="styleObject">
    <el-menu
      :default-active="activeMenu"
      :collapse="isCollapse"
      :router="true"
    >
      <sidebar-menu-item
        v-for="route in permission_routes"
        :key="route.path"
        :item="route"
        :base-path="route.path"
      />
    </el-menu>
  </el-aside>
</template>

<script>
import SidebarMenuItem from './SidebarMenuItem'

export default {
  name: 'Sidebar',
  props: {
    isCollapse: {
      type: Boolean,
      default: false
    },
    height: {
      type: String,
      default: 'auto'
    }
  },
  components: {
    SidebarMenuItem
  },
  data () {
    return {
      permission_routes: this.$store.state.permission.routes
    }
  },
  computed: {
    styleObject () {
      return {
        height: this.height
      }
    },
    activeMenu () {
      const route = this.$route
      const { meta, path } = route
      // if set path, the sidebar will highlight the path you set
      if (meta.activeMenu) {
        return meta.activeMenu
      }
      return path
    }
  }
}
</script>
