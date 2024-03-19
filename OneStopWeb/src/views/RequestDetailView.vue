<template>
    <div>
        <el-button style="position: fixed;">
            &lt;
        </el-button>
        <div class="rDetails">
            <main class="content">
                <h1>{{ request.category }}</h1>
                <div class="formRequest">
                    <p>kính gửi cơ quan chính phủ</p>
                    <p>Tôi: {{ state.username || '-' }} </p>
                    <p>Viết đơn này vì:</p>
                    <p>{{ request.reason }}</p>
                    <p>Tôi xin cảm ơn.</p>
                    <br />
                    <br />
                    <br />
                    <br />
                    <p>Đính kèm file: <a href="">{{ request.file }}</a></p>
                    <p>Thời gian gửi: {{ request.submittedAt }}</p>
                    <p>Trạng thái: {{ request.status }}</p>
                    <p>Quy trình xử lý: {{ request.processNote }}</p>
                </div>
            </main>
            <nav>
                <el-input type="textarea" placeholder="Enter your comment" />
                <el-button type="primary">Approve</el-button>
                <el-button type="danger">Reject</el-button>
            </nav>
        </div>
    </div>
</template>

<script>
import { useAuthStore } from '@/stores/auth';

export default {
    setup() {
        const { state } = useAuthStore();

        const request = {
            'id': 1,
            'category': "Hồ sơ lĩnh vực tài nguyên nước",
            'processNote': "Ok chờ đi em",
            'file': "",
            'reason': "Tôi muốn xin giấy phép khai thác nước sông Hồng để tưới tiêu cho vườn cây ăn quả của tôi. Tôi đã chuẩn bị đầy đủ giấy tờ và hồ sơ cần thiết. Mong cơ quan xem xét và giải quyết giúp tôi. Xin cảm ơn!",
            'submittedAt': "2024-03-20T00:54:37.7479046+07:00",
            'status': "Submitted"
        }

        return {
            request,
            state
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
        max-width: 1200px;


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
