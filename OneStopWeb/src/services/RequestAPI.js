import BaseService from "./BaseService";
import API_CODE from "../utils/api_code.js";

export default class RequestAPI extends BaseService {

    getRequestList(params = {}, success, error) {
        this.get(API_CODE.API_RQ_001, success, error, params);
    }

    getRequestDetail(id, params = {}, success, error) {
        this.get(API_CODE.API_RQ_003 + id, success, error, params);
    }

    fetchPreRequestData(params = {}, success, error) {
        this.get(API_CODE.API_CT_001, success, error, params);
    }

    submitRequest(params = {}, success, error) {
        this.post(API_CODE.API_RQ_002, success, error, params);
    }

    updateRequest(params = {}, success, error) {
        this.post(API_CODE.API_RQ_004, success, error, params);
    }

}
