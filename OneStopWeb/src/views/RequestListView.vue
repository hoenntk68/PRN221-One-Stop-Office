<template>
  <div>
    <RequestTable :requestList="anything"></RequestTable>
    <button @click="getRequestList">Get Request List</button>
  </div>
</template>
<script>
import RequestTable from '@/components/RequestTable.vue'
import { useRequestStore } from '@/stores/request.js'
import { computed, onMounted } from 'vue'
export default {
  setup() {
    // Call API get request list
    const requestStore = useRequestStore()

    const { getRequestList } = requestStore

    const anything = computed(() => requestStore.requestList.data)

    onMounted(async () => {
      await getRequestList()
    })

    return {
      getRequestList,
      anything
    }
  },
  components: { RequestTable }
}
</script>
<style></style>
