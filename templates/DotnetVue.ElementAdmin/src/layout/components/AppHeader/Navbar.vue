<template>
    <el-menu
        mode="horizontal"
        background-color="#2b303b"
        text-color="#fff"
        active-text-color="#fff">
        <el-submenu index="account">
            <template slot="title">
                <el-avatar shape="square" size="medium" :src="avatar" />
                <span>{{ name }}</span>
            </template>
            <el-menu-item>个人设置</el-menu-item>
            <el-menu-item @click="logout">
                安全登出
            </el-menu-item>
        </el-submenu>
    </el-menu>
</template>

<script>
import store from '../../../store'

export default {
  data () {
    return {
      name: this.$store.state.user.name,
      avatar: this.$store.state.user.avatar
    }
  },
  methods: {
    async logout () {
      await store.dispatch('user/resetToken')
      window.location.reload()
    }
  }
}
</script>
