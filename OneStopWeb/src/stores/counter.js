import { defineStore } from 'pinia'
import service from "../plugins/service"
import { reactive } from 'vue'

export const useCounterStore = defineStore('counter', () => {

  const requestModel = reactive({
    id: "1",
    category: "",
    reason: "",
    attachment: "",

  })

  const getRequest = () => {
    service.request.getRequest(
      requestModel.id,
      (res) => {
        console.log(res);
      },
      (err) => {
        console.log(err);
      }
    );
  }

  return {
    requestModel,
    getRequest,
  }
})
