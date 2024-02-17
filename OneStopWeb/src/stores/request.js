import { defineStore } from 'pinia'
import service from "../plugins/service"
import { reactive } from 'vue'

export const useRequestStore = defineStore('request', () => {
    const requestList = reactive({
        data: {},
      });

    const getRequestList = async () => {
        await service.request.getRequestList(
            {
            },
            (res) => {
                requestList.data = (res.data);
            },
            (err) => {
                console.log(err);
            }
        )
    }

    return {
        getRequestList,
        requestList
    }
})