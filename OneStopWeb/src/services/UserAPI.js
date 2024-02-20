import BaseService from "./BaseService";
import API_CODE from "../utils/api_code.js";

export default class UserApi extends BaseService {
    async login(params = {}, success, error) {
        await this.post(API_CODE.API_U_001, success, error, params);
    }

    
}
