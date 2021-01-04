<template>
  <div>
    <el-breadcrumb separator-class="el-icon-arrow-right">
      <el-breadcrumb-item :to="{ path: '/dashboard' }">Dashboard</el-breadcrumb-item>
      <el-breadcrumb-item>demo</el-breadcrumb-item>
      <el-breadcrumb-item>富文本编辑器</el-breadcrumb-item>
    </el-breadcrumb>
    <h1>富文本编辑器</h1>
    <el-divider />
    <div>
      wangeditor文档：
      <el-link type="primary" target="_blank" href="http://www.wangeditor.com/doc/">http://www.wangeditor.com/doc/</el-link>
    </div>
    <div style="margin: 30px 0">
      <rich-text-editor :html="richText" :change-handle="changeHandle" />
    </div>
    <el-divider content-position="left">内容预览</el-divider>
    <div class="preview_content" v-html="html" />
  </div>
</template>

<script>
import RichTextEditor from '@/components/RichTextEditor'
export default {
  name: 'RichTextEditorDemo',
  components: {
    RichTextEditor
  },
  data () {
    return {
      richText: '',
      html: '',
      text: ''
    }
  },
  methods: {
    changeHandle (html, text) {
      this.html = html
      this.text = text
    }
  }
}
</script>

<style>
  /* table 样式 */
  .preview_content table {
    border-top: 1px solid #ccc;
    border-left: 1px solid #ccc;
  }
  .preview_content table td,
  .preview_content table th {
    border-bottom: 1px solid #ccc;
    border-right: 1px solid #ccc;
    padding: 3px 5px;
  }
  .preview_content table th {
    border-bottom: 2px solid #ccc;
    text-align: center;
    background-color: #f1f1f1;
  }

  /* blockquote 样式 */
  .preview_content blockquote {
    display: block;
    border-left: 8px solid #d0e5f2;
    padding: 5px 10px;
    margin: 10px 0;
    line-height: 1.4;
    font-size: 100%;
    background-color: #f1f1f1;
  }

  /* code 样式 */
  .preview_content code {
    display: inline-block;
    *display: inline;
    *zoom: 1;
    background-color: #f1f1f1;
    border-radius: 3px;
    padding: 3px 5px;
    margin: 0 3px;
  }
  .preview_content pre code {
    display: block;
  }

  /* ul ol 样式 */
  .preview_content ul, ol {
    margin: 10px 0 10px 20px;
  }
</style>
