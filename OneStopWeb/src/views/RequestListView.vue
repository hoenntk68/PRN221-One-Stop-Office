<template>
  <div class="requestList">
    <RequestTable :requestList="request"></RequestTable>
    <div>
      <button @click="loadmore">loadmore</button>
    </div>
  </div>
</template>
<script>
import RequestTable from '@/components/RequestTable.vue'
import { useRequestStore } from '@/stores/request.js'
import { computed, onMounted } from 'vue'
export default {
  setup() {
    const requestStore = useRequestStore()

    const { getRequestList, loadmore } = requestStore

    const request = computed(() => requestStore.requestList.data)

    onMounted(async () => {
      getRequestList()
    })

    return {
      getRequestList,
      loadmore,
      request
    }
  },
  components: { RequestTable }
}
</script>
<style></style>
