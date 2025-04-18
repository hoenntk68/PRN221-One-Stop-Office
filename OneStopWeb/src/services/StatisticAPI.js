import BaseService from "./BaseService";
import API_CODE from "../utils/api_code.js";

export default class StatisticAPI extends BaseService {

    getCateStatistic(params = {}, success, error) {
        this.get(API_CODE.API_STA_002, success, error, params);
    }
    getStaffEf(params = {}, success, error) {
        this.get(API_CODE.API_STA_003, success, error, params);
    }
    getGeneralStat(params = {}, success, error) {
        this.get(API_CODE.API_STA_004, success, error, params);
    }
    getStatusState(params = {}, success, error) {
        this.get(API_CODE.API_STA_005, success, error, params);
    }
    getTimeStat(params = {}, success, error) {
        this.get(API_CODE.API_STA_006, success, error, params);
    }
}
