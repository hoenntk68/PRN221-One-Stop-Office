<template>
    <div class="wrapper">
        <main class="content">
            <h1>Send an application to Gorverment Administration dept (Gửi đơn cho Phòng quản lý chính phủ)</h1>
            <p><span style="color: #25C690;">Lưu ý: </span>V/v gửi đơn/email đến các phòng ban
                Bộ phận xử lý đơn sẽ trả lời đơn/ email của công dân trong vòng 48h (trừ đơn rút tiền, đơn phúc tra,
                chuyển cơ sở...).
                Để hạn chế SPAM, sẽ giãn thời gian trả lời đơn/email có tính chất SPAM theo nguyên tắc: Khi công dân
                gửi N đơn/email (N>1) cho cùng một yêu cầu thì thời gian trả lời trong vòng Nx48h.
                Vì vậy công dân cần cân nhắc trước khi gửi đơn/email với cùng một nội dung để nhận được trả lời/giải
                quyết nhanh nhất theo quy định.</p>
            <h2>Account balance (Số dư tài khoản): <span style="color: #25C690;">1 VNĐ</span></h2>
            <table>
                <tr>
                    <td class="section-header">Application type:</td>
                    <td class="section-info">
                        <el-select v-model="requestModel.category" placeholder="Select a category">
                            <el-option v-for="item in cateList.data" :key="item.categoryId" :label="item.categoryName"
                                :value="item.categoryId" />
                        </el-select>
                    </td>
                    <td class="section-desc">
                        <p>Phí để xử lý thủ tục này là: <span style="color: #25C690;">0 (VND)</span></p>
                    </td>
                </tr>
                <tr>
                    <td class="section-header">Reason (Lý do):</td>
                    <td class="section-info" colspan="2">
                        <el-input type="textarea" v-model="requestModel.reason" />
                    </td>
                </tr>
                <tr>
                    <td class="section-header">File Attach:</td>
                    <td class="section-info">
                        <input type="file" @change="handleFileUpload" />
                    </td>
                    <td class="section-desc">
                        <p>Extension File: <span style="color: #25C690;">"xlsx", "pdf", "docx", "doc", "xls",
                                "jpg", "png","zip". </span></p>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td></td>
                    <td>
                        <el-button @click="submitRequest">submit</el-button>
                    </td>
                </tr>
            </table>


        </main>
    </div>
</template>
<script>
import { useRequestStore } from '@/stores/request';
import { onMounted, ref } from 'vue';

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
            fetchPreRequestData,
            cateList,
        } = requestStore;

        onMounted(() => {
            fetchPreRequestData();
        });

        const submitRequest = () => {
            requestStore.submitRequest();
        }

        return {
            requestModel,
            submitRequest,
            fileInputRef,
            handleFileUpload,
            cateList,
            fetchPreRequestData,
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
    padding: 0;
    max-width: 1200px;

    padding: 4rem;
    border-radius: 2rem;
    margin: auto;
    margin-top: 16px;
    width: 100%;
    background-color: var(--color-background-soft);

    h2 {
        text-align: center;
    }

    .content {
        margin: 0;
        width: 100%;

        table {
            margin-top: 1rem;
            width: 100%;

            td {
                // padding: 8px 16px;
            }

            td:nth-child(2) {
                padding: 8px 16px;
            }
        }
    }
}
</style>