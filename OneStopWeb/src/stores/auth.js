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
        isAdmin: false,
        isSuperAdmin: false,
    })

    const authLogin = () => {
        mixinMethods.startLoading();
        service.user.login(
            {
                "user_name": credential.cccd,
                "password": credential.password,
            },
            (res) => {
                for (let key in res) {
                    Cookies.set(key, res[key], { expires: 7 });
                }
                location.href = '/';
                mixinMethods.endLoading();
                state.isLoggedin = !!Cookies.get('token');
                state.username = Cookies.get('fullname');
            },
            (err) => {
                mixinMethods.endLoading();
                state.isLoggedin = !!Cookies.get('token');
                $notify.error(err.responseCode || 'error');
            }
        )
    }

    const authLogout = () => {
        Cookies.remove('token');
        Cookies.remove('fullname');
        Cookies.remove('username');
        Cookies.remove('isAdmin');
        Cookies.remove('isSuperAdmin');
        state.isLoggedin = !!Cookies.get('token');
        state.username = Cookies.get('fullname') || '';
    }

    return {
        credential,
        authLogin,
        authLogout,
        state,
    }
})
