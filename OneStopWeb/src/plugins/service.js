import axios from "@/plugins/axios";
import RequestAPI from "@/services/RequestAPI";
import UserApi from "@/services/UserAPI";
import StatisticAPI from "@/services/StatisticAPI";
import CateApi from "@/services/CateAPI";

const service = {
    request: new RequestAPI(axios.axiosInstance, "/"),
    user: new UserApi(axios.axiosInstance, "/"),
    statistic: new StatisticAPI(axios.axiosInstance, "/"),
    cate: new CateApi(axios.axiosInstance, "/"),
}

export default service

