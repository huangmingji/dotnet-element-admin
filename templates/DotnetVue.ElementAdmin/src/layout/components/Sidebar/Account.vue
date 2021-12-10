<template>
  <div class="account-center-top__avatar">
    <el-row :gutter="10">
      <el-col :span="24">
        <avatar-upload :value="avatar" :action="11" :width="'100px'" :height="'100px'" />
      </el-col>
    </el-row>
    <el-row :gutter="10">
      <el-col :span="24">
        <span>{{ name }}</span>
      </el-col>
    </el-row>
    <el-row :gutter="10">
      <el-col :span="24">
        <el-dropdown size="mini" split-button type="default">
          <i class="el-icon-setting" /> 账号设置
          <el-dropdown-menu slot="dropdown">
            <el-dropdown-item>安全退出</el-dropdown-item>
          </el-dropdown-menu>
        </el-dropdown>
      </el-col>
    </el-row>
  </div>
</template>

<script>
import store from '../../../store'
import AvatarUpload from '../../../components/Upload/AvatarUpload'

export default {
  components: {
    AvatarUpload
  },
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
