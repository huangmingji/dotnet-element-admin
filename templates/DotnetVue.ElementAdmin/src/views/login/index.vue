<template>
    <div class="login-container">
        <el-alert
                v-if="showAlert"
                title="您正在使用的IE内核浏览器兼容性差，网页加载慢，为了您有更好的操作体验，请下载使用谷歌浏览器，如使用的是360浏览器、百度浏览器、搜狗浏览器、QQ浏览器等，请切换到极速模式。"
                type="error"
                show-icon
        />
        <div class="filter-container" style="padding: 120px 0 0 0">
            <el-card class="box-card" style="margin:30px auto; width: 400px;">
                <div class="title-container">
                    <h3 class="title">
                        {{ systemName }}
                    </h3>
                </div>
                <el-form ref="loginForm" :model="loginForm" :rules="loginRules" class="login-form" auto-complete="on" label-position="left">
                    <el-form-item prop="userName">
                        <el-input ref="userName" v-model="loginForm.userName" placeholder="请输入账号/手机号码/身份证号码" />
                    </el-form-item>

                    <el-form-item prop="password">
                        <el-input
                                ref="password"
                                v-model="loginForm.password"
                                placeholder="请输入密码"
                                show-password
                                auto-complete="off"
                                @keyup.enter.native="handleLogin"
                        />
                    </el-form-item>

                    <el-form-item>
                        <div>
                            <el-button :loading="loading" type="primary" style="width:100%;margin-bottom:10px;" @click.native.prevent="handleLogin">登 录</el-button>
                        </div>
                    </el-form-item>
                </el-form>
            </el-card>
        </div>
    </div>
</template>

<script>
import defaultSettings from '../../settings'

export default {
  name: 'Login',
  data () {
    const validateUsername = (rule, value, callback) => {
      if (value.length === 0) {
        callback(new Error('请输入登录账号/手机号'))
      } else {
        callback()
      }
    }
    const validatePassword = (rule, value, callback) => {
      if (value.length === 0) {
        callback(new Error('请输入登录密码'))
      } else {
        callback()
      }
    }
    return {
      systemName: defaultSettings.title,
      loginForm: {
        userName: 'admin',
        password: 'password'
      },
      loginRules: {
        userName: [{required: true, trigger: 'blur', validator: validateUsername}],
        password: [{required: true, trigger: 'blur', validator: validatePassword}]
      },
      loading: false,
      redirect: undefined,
      otherQuery: {}
    }
  },
  computed: {
    showAlert () {
      const ua = navigator.userAgent.toLowerCase()
      if (ua.match(/msie/) != null || ua.match(/trident/) != null) {
        return true
      }
      return false
    }
  },
  watch: {
    $route: {
      handler: function (route) {
        const query = route.query
        if (query) {
          this.redirect = query.redirect
          this.otherQuery = this.getOtherQuery(query)
        }
      },
      immediate: true
    }
  },
  methods: {
    handleLogin () {
      this.$refs.loginForm.validate(valid => {
        if (valid) {
          this.loading = true
          this.$store.dispatch('user/login', this.loginForm).then(response => {
            this.loading = false
            if (response.success) {
              this.$router.push({ path: this.redirect || '/dashboard', query: this.otherQuery })
            } else {
              this.$message({
                message: response.message,
                type: 'error'
              })
            }
          }).catch(() => {
            this.loading = false
          })
        } else {
          return false
        }
      })
    },
    getOtherQuery (query) {
      return Object.keys(query).reduce((acc, cur) => {
        if (cur !== 'redirect') {
          acc[cur] = query[cur]
        }
        return acc
      }, {})
    }
  }
}
</script>

<style>
.login-container {
    min-height: 100%;
    width: 100%;
    background-color: #ffffff;
    overflow: hidden;
}

.login-container .login-form {
    position: relative;
    width: 300px;
    max-width: 100%;
    margin: 0 auto;
    overflow: hidden;
}

.login-container .tips {
    font-size: 14px;
    color: #fff;
    margin-bottom: 10px;
}

.login-container .tips span:first-of-type {
    margin-right: 16px;
}

.login-container .svg-container {
    padding: 6px 5px 6px 15px;
    color: #889aa4;
    vertical-align: middle;
    width: 30px;
    display: inline-block;
}

.login-container .title-container {
    position: relative;
}

.login-container .title-container .title {
    font-size: 26px;
    color: #000000;
    margin: 0px auto 40px auto;
    text-align: center;
    font-weight: bold;
}

.login-container .show-pwd {
    position: absolute;
    right: 10px;
    top: 7px;
    font-size: 16px;
    color: #889aa4;
    cursor: pointer;
    user-select: none;
}

.el-dropdown-link {
    cursor: pointer;
    color: #409EFF;
}

.el-icon-arrow-down {
    font-size: 12px;
}

.login-container {
    background-color: rgb(240, 242, 245);
    background-image: url('../../../static/img/bg.svg');
    background-size: cover;
    background-repeat: no-repeat;
    background-position: center 110px;
}
</style>
