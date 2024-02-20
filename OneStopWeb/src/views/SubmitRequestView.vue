<template>
    <div class="wrapper">
        <el-input v-model="requestModel.category" placeholder="cate"></el-input>
        <el-input v-model="requestModel.reason" placeholder="reason"></el-input>

        <input type="file" @change="handleFileUpload" />

        <el-button @click="submitRequest">submit</el-button>
    </div>
</template>
<script>
import { useRequestStore } from '@/stores/request';
import { ref } from 'vue';

export default {
    setup() {
        const requestStore = useRequestStore();
        const fileInputRef = ref(null);

        const handleFileUpload = (event) => {
            const file = event.target.files[0];
            if (file) {
                requestModel.attachment = file;
            }
        };

        const {
            requestModel,
        } = requestStore;

        const submitRequest = () => {
            requestStore.submitRequest();
        }

        return {
            requestModel,
            submitRequest,
            fileInputRef,
            handleFileUpload,
        }
    }
}
</script>
<style lang="scss" scoped>
.wrapper {
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;

    max-width: 1000px;

    * {
        margin: 8px;
    }
}
</style>