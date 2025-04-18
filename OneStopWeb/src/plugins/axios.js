import axios from 'axios';
import Cookies from 'js-cookie';

const headers = {
    'x-locale': Cookies.get('CurrentLanguage') || 'ja'
};

const token = Cookies.get('token');
if (token != undefined && token.length != 0) {
    headers.Authorization = 'Bearer ' + token;
}

const axiosInstance = axios.create({
    baseURL: import.meta.env.VITE_BASE_URL,
    timeout: 60000,
    headers,
});

axiosInstance.interceptors.request.use(request => {
    if (token !== undefined) {
        request.headers.Authorization = `Bearer ${token}`;
    }
    return request;
});

const setHeaders = function (headers) {
    axiosInstance.defaults.headers.common = { ...axiosInstance.defaults.headers.common, ...headers };
}

export default {
    axiosInstance,
    setHeaders
};
