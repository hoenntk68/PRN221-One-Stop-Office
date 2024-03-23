<template lang="">
        <img alt="logo" class="logo" src="@/assets/homepage.png" width="1250px" height="600px" />
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

    ul {
        list-style: none;
        text-decoration: none;

        li {
            margin: 8px;
        }
    }

    button {
        background-color: #20C58E;
        color: #ffffff;
        font-size: 1.5rem;
        font-weight: bold;
        padding: 1.5rem;
        border-radius: 12px;

        &:hover {
            filter: brightness(95%);
        }
    }

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

        .right {}
    }
}
</style>
