<template>
    <div>
        <el-breadcrumb separator-class="el-icon-arrow-right">
            <el-breadcrumb-item :to="{ path: '/dashboard' }">Dashboard</el-breadcrumb-item>
            <el-breadcrumb-item>demo</el-breadcrumb-item>
            <el-breadcrumb-item>Upload 上传</el-breadcrumb-item>
        </el-breadcrumb>
        <h1>Upload 上传</h1>
        <el-divider />
        <div>
            <drag-upload
                :action="action"
                accept=".jpg,.png"
                :show-file-list="true"
                tip="只能上传jpg/png文件，且不超过500kb"
            />
        </div>
    </div>
</template>

<script>
import DragUpload from '@/components/Upload/DragUpload'
export default {
  name: 'UploadDemo',
  components: {
    DragUpload
  },
  data () {
    return {
      action: '/api/file-upload'
    }
  }
}
</script>
