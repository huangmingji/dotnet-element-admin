<template>
  <div class="account-center-top__avatar">
    <avatar-upload :value="avatar" :action="11" :width="'100px'" :height="'100px'" />
    <div>{{ name }}</div>
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
