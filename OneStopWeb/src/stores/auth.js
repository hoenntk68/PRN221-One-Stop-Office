import { defineStore } from 'pinia'
import service from "../plugins/service"
import { reactive } from 'vue'
import Cookies from 'js-cookie'
import router from '@/router'

export const useAuthStore = defineStore('auth', () => {

    const credential = reactive({
        cccd: '',
        password: '',
    })

    const authLogin = () => {
        service.user.login(
            {
                "user_name": credential.cccd,
                "password": credential.password,
            },
            (res) => {
                Cookies.set('access_token', res.token, { expires: 7 });
                router.push('home');
            },
            (err) => {
                console.log(err);
            }
        )
    }

    return {
        credential,
        authLogin,
    }
})
