import { AuthActions } from './../store/common/enums';
import axios from 'axios';
import router from '@/router';
import store from '@/store';

const instance = axios.create({
    baseURL: 'https://localhost:7031/'
});

instance.interceptors.request.use((config: any) => {
    if (store.getters.getAccessToken) {
        config.headers = {
            Authorization: 'Bearer ' + store.getters.getAccessToken
        };
    }
    return config;
});

let refresh = false;
instance.interceptors.response.use(undefined, async (error: any) => {
    if (error.response.status === 401 && error.config && !refresh) {
        refresh = true;
        try {
            await store.dispatch(AuthActions.REFRESH);
            return instance.request(error.config);
        } catch {
            try {
                await store.dispatch(AuthActions.LOGOUT);
            } finally {
                router.push('/auth');
            }
        } finally {
            refresh = false;
        }
    }
    return Promise.reject(error);
});

export default instance;