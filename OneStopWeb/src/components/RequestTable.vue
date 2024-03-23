<template>
  <div>
    <div class="requestTable">
      <div class="rthead">
        <span>
          <input type="checkbox" v-model="checkAll" @change="handleCheckAll">
          STT
        </span>
        <span>
          <el-select v-model="requestFilter.categoryId" @change="handleFilter()">
            <el-option label="All" value="0"></el-option>
            <el-option v-for="item in categoryList" :key="item.categoryId" :label="item.categoryName"
              :value="item.categoryId"></el-option>
          </el-select>
        </span>
        <span>Lý do</span>
        <span>Ghi chú Cán bộ</span>
        <el-select v-model="requestFilter.sortOption" @change="handleFilter()">
          <el-option label="Cũ" value="asc"></el-option>
          <el-option label="Mới" value="desc"></el-option>
        </el-select>
        <el-select v-model="requestFilter.status" @change="handleFilter()">
          <el-option label="All" value=""></el-option>
          <el-option label="Trạng thái: Submitted" value="Submitted"></el-option>
          <el-option label="Trạng thái: Approved" value="Approved"></el-option>
          <el-option label="Trạng thái: Rejected" value="Rejected"></el-option>
          <el-option label="Trạng thái: Processing" value="Processing"></el-option>
          <el-option label="Trạng thái: Cancelled" value="Cancelled"></el-option>
        </el-select>
        <span>Thao tác</span>
      </div>

    </div>
    <div class="request-table-body">
      <div v-for="(item, index) in requestList" :key="index" class="rtbody">
        <span style="text-align: center;">
          <input type="checkbox" v-model="selectedItems.data" class="index-checkbox" @change="handleCheckboxChange"
            :value="item.id">
          {{ item.id }}
        </span>
        <span>{{ item.category }}</span>
        <span>{{ item.reason }}</span>
        <span>{{ item.processNote }}</span>
        <span>{{ item.submittedAt }}</span>
        <span>{{ item.status }}</span>
        <div>
          <el-button type="primary" @click="navigateDetails(item.id)">Chi tiết</el-button>
          <el-button v-if="item.status != 'approved' && !state.isSuperAdmin" type="danger"
            @click="handleDelete(item.id, 'Cancelled')">Hủy yêu
            cầu</el-button>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import { onMounted, ref } from 'vue';
import { useRouter } from 'vue-router';
import { useRequestStore } from '@/stores/request';
import { useAuthStore } from '@/stores/auth';
import service from '../plugins/service';
import { $notify } from '@/utils/variables';

export default {
  props: { requestList: Object },
  setup() {
    const requestStore = useRequestStore();
    const {
      updateRequestStatus,
      requestState,
      requestFilter,
      selectedItems,
    } = requestStore;

    const { state } = useAuthStore();

    const checkAll = ref(false);

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

    const handleFilter = () => {
      requestStore.getRequestList();
    }

    const handleCheckAll = () => {
      const checkboxes = document.querySelectorAll('.index-checkbox');
      checkboxes.forEach(checkbox => {
        checkbox.checked = checkAll.value;
      });
    };

    const categoryList = ref([]);

    const fetchCategory = () => {
      try {
        service.cate.getAll(
          {},
          (response) => {
            categoryList.value = response;
          },
          (error) => {
            $notify.error('Error', error.message);
          }
        );
      } catch (error) {
        console.log('error', error);
        $notify.error('Error', error.message);
      }
    };

    const handleCheckboxChange = () => {
      console.log('selectedItems in component', selectedItems);
    };

    onMounted(() => {
      fetchCategory();
    });


    return {
      search,
      navigateDetails,
      handleEdit,
      handleDelete,
      requestState,
      requestFilter,
      handleFilter,
      selectedItems,
      handleCheckAll,
      handleCheckboxChange,
      checkAll,
      fetchCategory,
      categoryList,
      state,
    };
  }

}
</script>
<style lang="scss" scoped>
.rthead,
.rtbody {
  display: grid;
  grid-template-columns: .8fr 3fr 3fr 2fr 2fr 2fr 3fr;

  span {
    white-space: nowrap;
    overflow: hidden;
    text-overflow: ellipsis;
  }
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


  span {
    // padding: 0 8px;
  }
}

.request-table-body {
  height: 70dvh;
  overflow: auto;
}

.rtbody {
  margin: 8px 0;
  background-color: #d8e7e2;
  grid-template-rows: 1fr;
  padding: 1rem;
  border-radius: 8px;
}
</style>
