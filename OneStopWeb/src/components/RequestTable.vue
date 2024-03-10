<template>
  <div class="dark">
    <table>
      <thead>
        <th>ID</th>
        <th>Danh mục</th>
        <th>Lý do</th>
        <th>File đính kèm</th>
        <th>Thời gian tạo</th>
        <th>Trạng thái</th>
        <th>Thao tác</th>
      </thead>
      <tr v-for="(item, index) in requestList" :key="index">
        <td>{{ item.id }}</td>
        <td>{{ item.category }}</td>
        <td>{{ item.reason }}</td>
        <td>{{ item.file }}</td>
        <td>{{ item.creationTime }}</td>
        <td>{{ item.status }}</td>
        <td>{{ item.action }}</td>
      </tr>
    </table>
  </div>
</template>
<script>
export default {
  props: { requestList: Object }
}
</script>
<style></style>