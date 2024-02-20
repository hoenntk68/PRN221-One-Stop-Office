import { defineStore } from 'pinia'
import service from "../plugins/service"
import { reactive } from 'vue'

export const useRequestStore = defineStore('request', () => {

    const requestList = reactive({
        data: [],
    });

    const requestModel = reactive({
        'category': '',
        'reason': '',
        'attachment': '',
    })

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

    const submitRequest = async () => {
        const formData = new FormData();
        for (const key in requestModel) {
            formData.append(key, requestModel[key]);
        }
        service.request.submitRequest(
            formData,
            (res) => {
                console.log(res);
            },
            (err) => {
                console.log(err);
            }
        )
    }

    return {
        requestModel,
        requestList,
        getRequestList,
        submitRequest,
    }
})