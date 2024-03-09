import { defineStore } from 'pinia'
import service from "../plugins/service"
import { reactive } from 'vue'
import Cookies from 'js-cookie'
import router from '@/router'
import { mixinMethods, $notify } from '@/utils/variables';

export const useAuthStore = defineStore('auth', () => {

    const credential = reactive({
        cccd: '',
        password: '',
    })

    const state = reactive({
        isLoggedin: false,
        username: '',
    })

    const authLogin = () => {
        mixinMethods.startLoading();
        service.user.login(
            {
                "user_name": credential.cccd,
                "password": credential.password,
            },
            (res) => {
                Cookies.set('access_token', res.token, { expires: 7 });
                Cookies.set('username', res.username, { expires: 7 });
                Cookies.set('fullname', res.fullname, { expires: 7 });
                router.push('/');
                mixinMethods.endLoading();
                state.isLoggedin = !!Cookies.get('access_token');
                state.username = Cookies.get('fullname');
            },
            (err) => {
                mixinMethods.endLoading();
                state.isLoggedin = !!Cookies.get('access_token');
                $notify.error(err.responseCode || 'error');
            }
        )
    }

    const authLogout = () => {
        Cookies.remove('access_token');
        Cookies.remove('fullname');
        Cookies.remove('username');
        state.isLoggedin = !!Cookies.get('access_token');
        state.username = Cookies.get('fullname') || '';
    }

    return {
        credential,
        authLogin,
        authLogout,
        state,
    }
})
