import { defineStore } from 'pinia'
import service from "../plugins/service"
import { reactive, ref } from 'vue'
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
        choosenUser: '',
    });
    const selectedItems = reactive({
        data: [],
    });

    const requestModel = reactive({
        'category': '',
        'reason': '',
        'attachment': '',
    })

    const requestFilter = reactive({
        'status': 'Submitted',
        'orderBy': 'desc',
        'sortBy': '',
        'sortOption': '',
        'categoryId': '0',
    });

    const assignRequest = async () => {
        mixinMethods.startLoading();
        let data = {
            "Requests": selectedItems.data,
            "AssignedTo": requestState.choosenUser,
        }
        console.log(data);
        service.request.assignRequest(
            data,
            (res) => {
                mixinMethods.endLoading();
                $notify.success(res.message || 'success');
                getRequestList();
            },
            (err) => {
                mixinMethods.endLoading();
                $notify.error(err.responseCode || 'error');
            }
        );
    }


    const getRequestList = () => {
        mixinMethods.startLoading();
        service.request.getRequestList(
            {
                status: requestFilter.status,
                orderBy: requestFilter.orderBy,
                categoryId: requestFilter.categoryId,
            },
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
                status: requestFilter.status,
                orderBy: requestFilter.orderBy,
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

    const updateRequestStatus = async (status, statusNote = '') => {
        mixinMethods.startLoading();
        const requestData = {
            "RequestId": requestState.currentRequestId,
            "Status": status,
        };
        if (statusNote !== '') {
            requestData["ProcessNote"] = statusNote;
        }

        service.request.updateRequest(
            requestData,
            (res) => {
                mixinMethods.endLoading();
                $notify.success(res.message || 'success');
                getRequestList();
            },
            (err) => {
                mixinMethods.endLoading();
                $notify.error(err.responseCode || 'error');
            }
        );
    }

    const exportData = () => {
        mixinMethods.startLoading();
        service.request.exportRequest(
            {
                status: requestFilter.status,
                orderBy: requestFilter.orderBy,
                categoryId: requestFilter.categoryId,
            },
            (res) => {
                mixinMethods.endLoading();
                $notify.success(res.message || 'success');
            },
            (err) => {
                mixinMethods.endLoading();
                $notify.error(err.responseCode || 'error');
            }
        );
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
        requestFilter,
        selectedItems,
        exportData,
        assignRequest,
    }
})
