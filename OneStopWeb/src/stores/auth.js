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

    const test = () => {
        console.log(Cookies.get('access_token'));
    }

    const authLogin = () => {
        console.log(credential);
        service.user.login(
            {
                "user_name": credential.cccd,
                "password": credential.password,
            },
            (res) => {
                console.log(res);
                Cookies.set('access_token', res.token, { expires: 7 });
                router.push('home');
            },
            (err) => {
                console.log(err);
            }
        );
    }

    return {
        credential,
        authLogin,
        test,
    }
})
