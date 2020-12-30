<template>
    <el-menu-item v-if="!item.hidden && hasOneShowingChild(item.children, item)" :index="resolvePath(onlyOneChild.path)">
        <template v-if="onlyOneChild.meta">
            <i :class="convertIcon(onlyOneChild.meta)"/>
            <span slot="title">{{ convertTitle(onlyOneChild.meta) }}</span>
        </template>
        <template v-else>
            <span slot="title">{{ convertTitle(onlyOneChild.meta) }}</span>
        </template>
    </el-menu-item>

    <el-submenu v-else-if="!item.hidden" :index="resolvePath(item.path)">
        <template slot="title" v-if="item.meta">
            <i :class="convertIcon(item.meta)"/>
            <span slot="title">{{ convertTitle(item.meta)}}</span>
        </template>
        <template slot="title" v-else>
            <span slot="title">{{ convertTitle(item.meta) }}</span>
        </template>
        <sidebar-menu-item
          v-for="child in item.children"
          :key="child.path"
          :is-nest="true"
          :item="child"
          :base-path="resolvePath(item.path)"/>
    </el-submenu>
</template>

<script>
import path from 'path'
import { isExternal } from '../../../utils/validate'
import SidebarMenuItem from './SidebarMenuItem'

export default {
  name: 'SidebarMenuItem',
  components: {
    SidebarMenuItem
  },
  props: {
    item: {
      type: Object,
      required: true
    },
    isNest: {
      type: Boolean,
      default: false
    },
    basePath: {
      type: String,
      default: ''
    }
  },
  data () {
    return {
      onlyOneChild: null
    }
  },
  methods: {
    hasOneShowingChild (children = [], parent) {
      const showingChildren = children.filter(item => {
        if (item.hidden) {
          return false
        } else {
          // Temp set(will be used if only has one showing child)
          this.onlyOneChild = item
          return true
        }
      })

      // When there is only one child router, the child router is displayed by default
      if (showingChildren.length === 1) {
        return true
      }

      // Show parent if there are no child router to display
      if (showingChildren.length === 0) {
        this.onlyOneChild = Object.assign({}, parent)
        return true
      }

      return false
    },
    resolvePath (routePath) {
      if (isExternal(routePath)) {
        return routePath
      }
      if (isExternal(this.basePath)) {
        return this.basePath
      }
      return path.resolve(this.basePath, routePath)
    },
    convertTitle (meta) {
      if (meta && meta.title) {
        return meta.title
      }
      return ''
    },
    convertIcon (meta) {
      if (meta && meta.icon) {
        return meta.icon
      }
      return ''
    }
  }
}
</script>
