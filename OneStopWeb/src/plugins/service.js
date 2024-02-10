import axios from "@/plugins/axios";
import RequestAPI from "@/services/RequestAPI";


const service = {
    request: new RequestAPI(axios.axiosInstance, "/")

}

export default service

