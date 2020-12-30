<template>
    <el-upload
        drag
        ref="upload"
        :accept="accept"
        :action="action"
        :headers="headers"
        :show-file-list="showFileList"
        :on-exceed="handleExceed"
        :on-success="handleSuccess"
        :on-error="handleFail"
        multiple
    >
        <i class="el-icon-upload"></i>
        <div class="el-upload__text">将文件拖到此处，或<em>点击上传</em></div>
        <div class="el-upload__tip" slot="tip">{{ tip }}</div>
    </el-upload>
</template>

<script>
import {getToken} from '../../utils/auth'

export default {
  name: 'DragUpload',
  props: {
    action: {
      type: String,
      default: ''
    },
    accept: {
      type: String,
      default: ''
    },
    tip: {
      type: String,
      default: ''
    },
    limit: {
      type: String,
      default: '1'
    },
    showFileList: {
      type: Boolean,
      default: true
    }
  },
  computed: {
    headers () {
      return {
        'Authorization': 'Bearer ' + getToken()
      }
    }
  },
  methods: {
    handleSuccess (response, file) {
      if (!this.showFileList) {
        this.$refs.upload.clearFiles()
      }
      if (!response.success) {
        this.$message.error(response.message)
        return
      }
      this.$message.success(response.message || '上传成功')
    },
    handleFail (err, file, fileList) {
      if (!this.showFileList) {
        this.$refs.upload.clearFiles()
      }
      this.$message.error(err.message || '上传失败')
    },
    handleExceed (files, fileList) {
      // 上传文件个数超过定义的数量
      this.$message.warning('当前限制选择 ' + this.limit + ' 个文件，请删除后继续上传')
    }
  }
}
</script>
