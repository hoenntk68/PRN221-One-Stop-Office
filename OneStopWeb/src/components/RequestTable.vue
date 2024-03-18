<template>
  <div class="requestTable">
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
//change to commit
</script>
<style lang="scss" scoped>
.requestTable {
  padding: .5rem;

  table {
    width: 100%;
    border-collapse: collapse;

    .item {
      margin: 1rem;
    }

    tr {}

    th,
    td {
      border: 1px solid #ddd;
      padding: 8px;
      text-align: center;
    }

    th {
      background-color: #f2f2f2;
    }
  }
}
</style>