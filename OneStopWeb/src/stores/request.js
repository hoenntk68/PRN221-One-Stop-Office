import { defineStore } from 'pinia'
import service from "../plugins/service"
import { reactive } from 'vue'

export const useRequestStore = defineStore('request', () => {

    const requestList = reactive({
        data: [],
    });


    const getRequestList = () => {
        service.request.getRequestList(
            {
            },
            (res) => {
                console.log(res);
                requestList.data = requestList.data.concat(res);
            },
            (err) => {
                console.log(err);
            }
        )
    }

    return {
        getRequestList,
        requestList,
    }
})