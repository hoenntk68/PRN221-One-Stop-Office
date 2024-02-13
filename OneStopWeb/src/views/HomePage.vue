<template lang="">
    <div>
        <el-button @click="test">test</el-button>
    </div>
</template>
<script>
import { useAuthStore } from "../stores/auth";
export default {
    setup() {
        const authStore = useAuthStore();

        const {
            credential,
            authLogin,
            test,
        } = authStore;


        return {
            credential,
            authLogin,
            test,
        }
    }
}
</script>
<style lang="">
    
</style>