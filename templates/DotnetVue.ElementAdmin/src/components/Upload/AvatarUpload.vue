<template>
  <el-upload
      class="avatar-uploader"
      :accept="accept"
      :action="action"
      :headers="headers"
      :show-file-list="false"
      :auto-upload="autoUpload"
      :on-success="handleAvatarSuccess"
      :before-upload="beforeAvatarUpload"
      :on-error="handleFail"
  >
    <el-avatar shape="circle" v-if="imageUrl" :src="imageUrl" class="avatar" sha :style="avatarStyle"></el-avatar>
    <i v-else class="el-icon-plus avatar-uploader-icon"></i>
  </el-upload>
</template>

<script>
import {getToken} from '../../utils/auth'
import {formatFileSize} from '../../utils'

export default {
  name: 'AvatarUpload',
  props: {
    action: {
      type: String,
      default: ''
    },
    value: {
      type: String,
      default: ''
    },
    autoUpload: {
      type: Boolean,
      default: true
    },
    width: {
      type: String,
      default: '178px'
    },
    height: {
      type: String,
      default: '178px'
    }
  },
  data () {
    return {
      fileSizeLimit: 1024,
      imageUrl: '',
      accept: 'image/*',
      tip: ''
    }
  },
  watch: {
    value (value) {
      this.imageUrl = value
    }
  },
  created () {
    this.imageUrl = this.value
  },
  computed: {
    headers () {
      return {
        'Authorization': 'Bearer ' + getToken()
      }
    },
    avatarStyle () {
      return {
        'width': this.width,
        'height': this.height
      }
    }
  },
  methods: {
    async handleAvatarSuccess (res, file) {
      this.imageUrl = URL.createObjectURL(file.raw)
      this.emitInput(this.imageUrl)
    },
    beforeAvatarUpload (file) {
      // const isJPG = file.type === 'image/jpeg'
      // const isLt2M = file.size / 1024 / 1024 < 2
      //
      // if (!isJPG) {
      //   this.$message.error('上传头像图片只能是 JPG 格式!')
      // }
      // if (!isLt2M) {
      //   this.$message.error('上传头像图片大小不能超过 2MB!')
      // }
      // return isJPG && isLt2M
      const isLtSize = file.size < (this.fileSizeLimit * 1024)
      if (!isLtSize) {
        const limitSize = formatFileSize(this.fileSizeLimit * 1024)
        this.$message.error('上传图片大小不能超过 ' + limitSize + '!')
      }
      return isLtSize
    },
    handleFail (err, file, fileList) {
      this.$message.error(err.message || '上传失败')
    },
    emitInput (val) {
      this.$emit('input', val)
    }
  }
}
</script>

<style>
.avatar-uploader .el-upload {
  /* border: 1px dashed #d9d9d9; */
  border-radius: 6px;
  cursor: pointer;
  position: relative;
  overflow: hidden;
}
.avatar-uploader .el-upload:hover {
  border-color: #409EFF;
}
.avatar-uploader-icon {
  font-size: 28px;
  color: #8c939d;
  width: 178px;
  height: 178px;
  line-height: 178px;
  text-align: center;
}
.avatar {
  width: 178px;
  height: 178px;
  display: block;
}
</style>
