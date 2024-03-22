import BaseService from "./BaseService.js";
import API_CODE from "../utils/api_code.js";

export default class CateAPI extends BaseService {

    getAll(params = {}, success, error) {
        this.get(API_CODE.API_CT_001, success, error, params);
    }

}
