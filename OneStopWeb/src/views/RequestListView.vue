<template>
  <div class="requestList">
    <RequestTable :requestList="request"></RequestTable>
    <div class="botton-btn">
      <el-button @click="loadmore">Tải Thêm</el-button>
      <el-button @click="handleAssign()">Gán cho staff</el-button>
      <el-button @click="export2()">Xuất dữ liệu</el-button>
    </div>

    <el-dialog title="Shipping address" width="500" v-model="dialogFormVisible" style="z-index: 1000000;">
      <template #default>
        <el-select v-model="requestState.choosenUser" laceholder="Please select a staff">
          <el-option v-for="item in listUser" :key="item.userId" :label="item.fullName" :value="item.userId" />
        </el-select>
      </template>
      <template #footer>
        <div class="dialog-footer">
          <el-button @click="dialogFormVisible = false">Hủy</el-button>
          <el-button type="primary" @click="confirmAssign()">Đồng ý</el-button>
        </div>
      </template>
    </el-dialog>

  </div>
</template>

<script>
import RequestTable from '@/components/RequestTable.vue'
import { useRequestStore } from '@/stores/request.js'
import { computed, onMounted, ref } from 'vue'
import { useAuthStore } from '@/stores/auth';
import Cookies from 'js-cookie';

export default {
  components: { RequestTable },
  setup() {
    const requestStore = useRequestStore()

    const authStore = useAuthStore();
    const {
      getUserList,
    } = authStore;
    const listUser = computed(() => authStore.listUser)

    const { getRequestList, loadmore, selectedItems, requestState } = requestStore

    const request = computed(() => requestStore.requestList.data)
    const filters = computed(() => requestStore.requestFilter)

    const dialogFormVisible = ref(false)

    const handleAssign = () => {
      dialogFormVisible.value = true;
      console.log('selectedItems', selectedItems)
    }

    const confirmAssign = () => {
      requestStore.assignRequest();
    }

    const handleExport = () => {
      requestStore.exportData();
    }

    const export2 = () => {
      let baseURL = import.meta.env.VITE_BASE_URL;
      let token = Cookies.get('token');
      let status = 1;
      if (!token) {
        console.error("Token not found. Please log in.");
        return;
      }
      // let exportURL = `${baseURL}/Request/export?jsonClaims=${token}&status=Cancelled&sortOption=desc`;
      let exportURL = `${baseURL}/Request/export?token=${token}&status=${requestStore.requestFilter.status}&sortBy=${requestStore.requestFilter.sortBy}&sortOption=${requestStore.requestFilter.orderBy}&categoryId=${requestStore.requestFilter.categoryId}`;
      window.location.href = exportURL;
    }


    onMounted(async () => {
      getRequestList();
      getUserList();
    })

    return {
      getRequestList,
      loadmore,
      request,
      selectedItems,
      handleAssign,
      dialogFormVisible,
      filters,
      handleExport,
      listUser,
      getUserList,
      requestState,
      confirmAssign,
      export2,
    }
  }
}
</script>

<style lang="scss">
.botton-btn {
  display: flex;
  justify-content: center;
  margin-top: 8px;

  button {
    margin: 0 8px;
  }
}
</style>
