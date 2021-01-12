<template>
    <div ref="editor" style="text-align:left;" />
</template>

<script>
import Editor from 'wangeditor'
import { getToken } from '../../utils/auth'

export default {
  name: 'RichTextEditor',
  props: {
    html: {
      type: String,
      default: ''
    },
    changeHandle: {
      type: Function,
      default: function (html, text) { }
    },
    height: {
      type: String,
      default: '300'
    }
  },
  data () {
    return {
      editor: [],
      defaultMenus: [
        'head', // 标题
        'bold', // 粗体
        'fontSize', // 字号
        'fontName', // 字体
        'italic', // 斜体
        'underline', // 下划线
        'strikeThrough', // 删除线
        'foreColor', // 文字颜色
        'backColor', // 背景颜色
        'indent', // 设置缩进
        'lineHeight', // 设置行高
        'list', // 列表
        'justify', // 对齐方式
        'quote', // 引用
        'image', // 插入图片
        'table', // 表格
        'undo', // 撤销
        'redo' // 重复
      ]
    }
  },
  mounted () {
    this.editor = new Editor(this.$refs.editor)
    this.editor.config.menus = this.defaultMenus
    this.editor.config.uploadImgServer = '/api/rich-text-editor/upload' // 上传图片到服务器
    this.editor.config.height = this.height
    this.editor.config.showLinkImg = false
    this.editor.config.pasteIgnoreImg = true
    this.editor.config.uploadImgMaxSize = 1 * 1024 * 1024
    this.editor.config.uploadImgMaxLength = 5
    this.editor.config.uploadImgHeaders = {
      'Authorization': 'Bearer ' + getToken()
    }
    this.editor.config.uploadImgHooks = {
      customInsert: function (insertImg, result, editor) {
        if (result.success) {
          result.data.forEach(url => {
            insertImg(url)
          })
        } else {
          this.$notify({
            title: '失败',
            message: result.message,
            type: 'error'
          })
        }
      }
    }
    this.editor.config.onchange = (html) => {
      this.changeHandle(html, this.editor.txt.text())
    }
    this.editor.create()
    this.editor.txt.html(this.html)
  },
  destroyed () {
    this.editor.destroy()
  }
}
</script>

<style scoped>

</style>
