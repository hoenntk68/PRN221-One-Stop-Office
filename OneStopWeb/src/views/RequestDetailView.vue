<template>
    <div>
        <el-button style="position: fixed;" @click="$router.push('/request-list')">
            &lt;
        </el-button>
        <div class="rDetails">
            <main class="content">
                <h3>Cộng Hòa Xã Hội Chủ Nghĩa Việt Nam</h3>
                <h3>Độc Lập - Tự Do - Hạnh Phúc</h3>
                <h1>{{ requestDetail.data.categoryName }}</h1>
                <br />
                <!-- {{ requestDetail }} -->
                <div class="formRequest">
                    <p>kính gửi cơ quan chính phủ</p>
                    <p>Tôi: {{ requestDetail.data.userFullName || '-' }} </p>
                    <p>Viết đơn này vì:</p>
                    <p>{{ requestDetail.data.reason }}</p>
                    <p>Tôi xin cảm ơn.</p>
                    <br />
                    <br />
                    <br />
                    <br />
                    <p>Đính kèm file:
                        <a @click="downloadAttachment(requestDetail.data.requestId)" style="cursor: pointer;">
                            {{ removePathPrefix(requestDetail.data.attachment || '') }}
                        </a>
                    </p>
                    <p>Thời gian gửi: {{ requestDetail.data.createdAt }}</p>
                    <p>Trạng thái: {{ requestDetail.data.status }}</p>
                    <p>Quy trình xử lý: {{ requestDetail.data.processNote }}</p>
                </div>
            </main>
            <nav
                v-if="state.isAdmin && requestDetail.data.status != 'Approved' && requestDetail.data.status != 'Rejected'">
                <el-input type="textarea" v-model="statusNote" placeholder="Enter your comment" />
                <el-button type="primary"
                    @click="processRequest(requestDetail.data.requestId, 'Approved')">Approve</el-button>
                <el-button type="danger"
                    @click="processRequest(requestDetail.data.requestId, 'Rejected')">Reject</el-button>
            </nav>
            <nav>
                <el-select v-model="requestDetail.assigned" placeholder="Select status">
                    <el-option v-for="item in listUser" :key="item.userId" :label="item.fullName"
                        :value="item.userId"></el-option>
                </el-select>
                <el-button type="primary" @click="reassign()">
                    Update
                </el-button>
            </nav>
        </div>
    </div>
</template>

<script>
import { useAuthStore } from '@/stores/auth';
import { useRequestStore } from '@/stores/request';
import { computed, onMounted, ref } from 'vue';
import { useRouter } from 'vue-router';

export default {
    setup() {
        const router = useRouter();
        const authStore = useAuthStore();
        const { state, getUserList, reAssign } = authStore;

        const listUser = computed(() => authStore.listUser);

        const requestStore = useRequestStore();
        const {
            getRequestDetail,
            requestDetail,
            updateRequestStatus,
        } = requestStore;

        const statusNote = ref('');

        const downloadAttachment = (id) => {
            let baseURL = import.meta.env.VITE_BASE_URL;
            location.href = `${baseURL}/Request/${id}/download`;
        }

        const removePathPrefix = (fileName) => {
            let serverPath = import.meta.env.VITE_BASE_UPLOAD_URL || "";
            fileName = fileName.replace(serverPath, '');

            return fileName;
        };

        onMounted(() => {
            let currentId = router.currentRoute.value.params.id;
            getRequestDetail(currentId);
            getUserList();
        });

        const processRequest = (id, status) => {
            requestStore.requestState.currentRequestId = id;
            updateRequestStatus(status, statusNote.value);
        }

        const reassign = () => {
            let data = {
                request_id: router.currentRoute.value.params.id,
                assigned_to: requestDetail.assigned,
            }
            requestStore.reAssign(data);
        }

        return {
            reassign,
            requestDetail,
            state,
            getRequestDetail,
            downloadAttachment,
            removePathPrefix,
            statusNote,
            processRequest,
            listUser,
            getUserList,
            reAssign,
        }
    }
}
</script>

<style lang="scss" scoped>
.rDetails {
    width: 100%;
    height: 100%;

    .content {
        display: flex;
        flex-direction: column;
        justify-content: center;
        align-items: center;
        padding: 0;
        max-width: 1000px;


        padding: 4rem;
        border-radius: 2rem;
        margin: auto;
        margin-top: 16px;
        width: 100%;
        background-color: var(--color-background-soft);

        .formRequest {
            width: 100%;
        }
    }

    nav {
        background-color: rgb(198, 223, 214);
        width: 100%;
        max-width: 1200px;
        display: flex;
        flex-direction: row;
        justify-content: center;
        align-items: center;
        margin: 2rem auto;
        padding: 1rem;
        border-radius: 1rem;

        button {
            margin: 0;
            margin-left: 1rem;
        }
    }
}
</style>
