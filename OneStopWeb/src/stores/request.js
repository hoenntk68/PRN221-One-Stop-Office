import { defineStore } from 'pinia'
import service from "../plugins/service"
import { reactive } from 'vue'
import { mixinMethods, $notify } from '@/utils/variables';

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
        mixinMethods.startLoading();
        service.request.getRequestList(
            {},
            (res) => {
                requestList.data = requestList.data.concat(res);
                mixinMethods.endLoading();
            },
            (err) => {
                mixinMethods.endLoading();
                $notify.error(err.responseCode || 'error');
            }
        )
    }

    const submitRequest = async () => {
        mixinMethods.startLoading();
        const formData = new FormData();
        for (const key in requestModel) {
            formData.append(key, requestModel[key]);
        }
        service.request.submitRequest(
            formData,
            (res) => {
                mixinMethods.endLoading();
                $notify.success(res.message || 'success');
            },
            (err) => {
                mixinMethods.endLoading();
                $notify.error(err.responseCode || 'error');
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