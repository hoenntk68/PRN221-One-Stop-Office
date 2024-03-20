import { defineStore } from 'pinia'
import service from "../plugins/service"
import { reactive } from 'vue'
import { mixinMethods, $notify } from '@/utils/variables';

export const useRequestStore = defineStore('request', () => {

    const requestList = reactive({
        data: [],
    });
    const requestDetail = reactive({
        data: [],
    });
    const cateList = reactive({
        data: [],
    });
    const requestState = reactive({
        currentRequestId: 0,
        ProcessNote: '',
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
                requestList.data = res;
                mixinMethods.endLoading();
            },
            (err) => {
                mixinMethods.endLoading();
                $notify.error(err.responseCode || 'error');
            }
        )
    }

    const loadmore = () => {
        mixinMethods.startLoading();
        service.request.getRequestList(
            {
                limit: 10,
                offset: requestList.data.length,
            },
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

    const getRequestDetail = (id) => {
        mixinMethods.startLoading();
        service.request.getRequestDetail(
            id,
            {},
            (res) => {
                requestDetail.data = res;
                mixinMethods.endLoading();
            },
            (err) => {
                mixinMethods.endLoading();
                $notify.error(err.responseCode || 'error');
            }
        )
    }

    const fetchPreRequestData = () => {
        mixinMethods.startLoading();
        service.request.fetchPreRequestData(
            {},
            (res) => {
                cateList.data = res;
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

    const updateRequestStatus = async (status) => {
        mixinMethods.startLoading();
        service.request.updateRequest(
            {
                "RequestId": requestState.currentRequestId,
                "Status": status,
            },
            (res) => {
                mixinMethods.endLoading();
                $notify.success(res.message || 'success');
                getRequestList();
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
        requestDetail,
        getRequestList,
        submitRequest,
        cateList,
        fetchPreRequestData,
        loadmore,
        getRequestDetail,
        requestState,
        updateRequestStatus,
    }
})
