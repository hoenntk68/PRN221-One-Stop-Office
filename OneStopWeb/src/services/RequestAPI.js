import BaseService from "./BaseService";
import API_CODE from "../utils/api_code.js";

export default class RequestAPI extends BaseService {

    getRequestList(params = {}, success, error) {
        this.get(API_CODE.API_RQ_001, success, error, params);
    }

}
