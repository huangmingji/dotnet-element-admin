<template>
  <el-main :style="styleObject">
    <router-view :key="key" />
  </el-main>
</template>

<script>
export default {
  name: 'AppMain',
  props: {
    height: {
      type: String,
      default: 'auto'
    }
  },
  computed: {
    styleObject () {
      return {
        height: this.height
      }
    },
    key () {
      return this.$route.fullPath
    }
  }
}
</script>
