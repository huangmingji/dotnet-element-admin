<template>
  <el-main :style="styleObject">
    <router-view :key="key" />
  </el-main>
</template>

<script>
export default {
  name: 'AppMain',
  computed: {
    styleObject () {
      const height = window.innerHeight - 60
      return {
        'overflow-y': 'scroll',
        height: height + 'px',
        width: 'auto'
      }
    },
    key () {
      return this.$route.fullPath
    }
  }
}
</script>
