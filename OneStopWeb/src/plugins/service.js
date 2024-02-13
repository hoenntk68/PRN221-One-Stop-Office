import axios from "@/plugins/axios";
import RequestAPI from "@/services/RequestAPI";
import UserApi from "@/services/UserAPI";

const service = {
    request: new RequestAPI(axios.axiosInstance, "/"),
    user: new UserApi(axios.axiosInstance, "/"),

}

export default service

