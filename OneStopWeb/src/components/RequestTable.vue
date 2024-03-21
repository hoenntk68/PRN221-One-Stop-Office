<template>
  <div>
    <div class="requestTable">
      <div>
        <input id="search" v-model="search" placeholder="Tìm kiếm" clearable />
        <el-button style="margin: 0 8px; background-color: #d8e7e2; border: 2px solid #0b9c6c;">Search</el-button>
      </div>
      <div class="rthead">
        <span>STT</span>
        <span>Danh mục</span>
        <span>Lý do</span>
        <span>File đính kèm</span>
        <span>Thời gian tạo</span>
        <span>Trạng thái</span>
        <span>Thao tác</span>
      </div>

    </div>
    <div v-for="(item, index) in requestList" :key="index" class="rtbody">
      <span style="text-align: center;">{{ item.id }}</span>
      <span>{{ item.category }}</span>
      <span>{{ item.reason }}</span>
      <span>{{ item.file }}</span>
      <span>{{ item.creationTime }}</span>
      <span>{{ item.status }}</span>
      <div>
        <el-button type="primary" @click="navigateDetails(item.id)">info</el-button>
        <el-button v-if="item.status != 'approved'" type="danger"
          @click="handleDelete(item.id, 'Cancelled')">Cancel</el-button>
      </div>
    </div>
  </div>
</template>
<script>
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import { useRequestStore } from '@/stores/request';

export default {
  props: { requestList: Object },
  setup() {
    const requestStore = useRequestStore();
    const { updateRequestStatus, requestState } = requestStore;

    const search = ref('');
    const router = useRouter();

    const navigateDetails = (id) => {
      router.push({ name: 'request-details', params: { id: id } });
    }

    const handleEdit = (index) => {
      console.log('edit', index);
    }

    const handleDelete = (id, status) => {
      requestStore.requestState.currentRequestId = id;
      updateRequestStatus(status);
    }


    return {
      search,
      navigateDetails,
      handleEdit,
      handleDelete,
      requestState,
    };
  }

}
</script>
<style lang="scss" scoped>
.rthead,
.rtbody {
  display: grid;
  grid-template-columns: .8fr 3fr 3fr 2fr 2fr 2fr 3fr;
}

.rthead {
  padding: 0 !important;

}

.requestTable {
  background-color: #25C690;
  padding: 1rem;
  border-radius: 1rem;
  margin-top: 8px;
  font-size: 1.2rem;
  font-weight: bolder !important;
  padding: 8px 24px;

  #search {
    background-color: #d8e7e2;
    border: 2px solid #0b9c6c;
    border-radius: 4px;
    height: 32px;
    line-height: 32px;
    font-size: 14px;
    padding: 0 10px;
    width: 25%;
  }

  span {
    // padding: 0 8px;
  }
}

.rtbody {
  margin: 8px 0;
  background-color: #d8e7e2;
  grid-template-rows: 1fr;
  padding: 1rem;
  border-radius: 8px;
}
</style>
