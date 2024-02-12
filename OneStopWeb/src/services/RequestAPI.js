import BaseService from "./BaseService";
import API_CODE from "../utils/api_code.js";

export default class RequestAPI extends BaseService {
    async getRequest(params = {}, success, error) {
        await this.post(API_CODE.API_RQ_001, params, success, error);
    }


}
