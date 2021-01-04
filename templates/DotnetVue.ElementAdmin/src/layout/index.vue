<template>
  <el-container direction="vertical">
    <app-header :is-collapse="isCollapse" :collapse-handle="collapseHandle" />
    <el-container>
      <sidebar :is-collapse="isCollapse" :height="height" />
      <app-main :height="height" />
    </el-container>
  </el-container>
</template>

<script>
import { AppHeader, Sidebar, AppMain } from './components'

export default {
  name: 'Layout',
  components: {
    AppHeader,
    Sidebar,
    AppMain
  },
  data () {
    return {
      isCollapse: false
    }
  },
  computed: {
    height () {
      const height = window.innerHeight - 60
      return height + 'px'
    }
  },
  methods: {
    collapseHandle (val) {
      this.isCollapse = val
    }
  }
}
</script>

<style>
  .el-header, .el-footer {
    background-color: #2b303b;
    color: #333;
    line-height: 60px;
  }

  .el-header .logo {
    color: #ffffff;
    text-decoration: none;
    margin-right: 20px;
  }

  .el-header .logo .sidebar-logo {
    width: 32px;
    height: 32px;
    vertical-align: middle;
  }

  .el-header .el-menu {
    float: right;
  }
  .el-header .el-menu>li {
    background-color: #B3C0D1;
    color: #333;
  }

  .el-aside {
    color: #333;
    line-height: 200px;
    text-align: left;
    border-right: 1px solid #e6e6e6;
    background-color: #f9fafc;
    max-width: 200px;
    height: 100vh;
    overflow-y: scroll;
  }

  .el-aside .el-menu {
    border: 0;
    background-color: #f9fafc;
  }

  .el-aside .el-menu:not(.el-menu--collapse) {
    border: 0;
    width: 200px;
  }

  .el-main {
    color: #333;
    width: auto;
    height: 100vh;
    overflow-y: scroll;
  }

  .el-container {
    margin: 0;
    padding: 0;
  }

  .el-container:nth-child(5) .el-aside,
  .el-container:nth-child(6) .el-aside {
    line-height: 260px;
  }

  .el-container:nth-child(7) .el-aside {
    line-height: 320px;
  }

</style>
