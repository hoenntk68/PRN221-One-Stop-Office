import BaseService from "./BaseService";
import API_CODE from "../utils/api_code.js";

export default class RequestAPI extends BaseService {
    async getList(success, error, params = {}) {
        await this.get(API_CODE.API_RQ_001, success, error, params);
    }


}
