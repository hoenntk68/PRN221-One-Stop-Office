import BaseService from "./BaseService";
import API_CODE from "../utils/api_code.js";

export default class StatisticAPI extends BaseService {

    getCateStatistic(params = {}, success, error) {
        this.get(API_CODE.API_STA_002, success, error, params);
    }

}
