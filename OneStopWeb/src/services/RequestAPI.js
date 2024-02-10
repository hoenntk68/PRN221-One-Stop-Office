import BaseService from "./BaseService";
import API_CODE from "../utils/api_code.js";

export default class RequestAPI extends BaseService {
    async getRequest(id, success, error, params = {}) {
        console.log(id, success, error, params);
        await this.get(API_CODE.API_RQ_001 + id, success, error, params);
    }

    
}
