<template lang="">
    <div class="wrapper">
        <div class="section left">
            <el-button @click="$router.push({ name: 'submitRequest' })">
                new request
            </el-button>
        </div>
        <div class="section right">
            <el-input style="width: 200px">

            </el-input>
            <el-button @click="$router.push({ name: 'requestList' })">
                tra cuu
            </el-button>
        </div>
    </div>
</template>
<script>
export default {
    setup() {
        return {
        }
    }
}
</script>
<style lang="scss" scoped>
.wrapper {
    display: flex;
    flex-direction: row;
    justify-content: center;
    align-items: center;
    height: 100%;

    .section {
        width: 50%;
        height: 100%;

        display: flex;
        flex-direction: column;
        justify-content: center;
        align-items: center;

        .left {
            button {
                margin: auto;
            }
        }

        .right {
            input {}
        }
    }
}
</style>