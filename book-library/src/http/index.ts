import axios from 'axios';
import router from '@/router';
import store from '@/store';

const instance = axios.create({
    baseURL: 'https://localhost:7031/',
    withCredentials: true
});

instance.interceptors.request.use((config: any) => {
    if (store.getters.StateToken) {
        config.headers = {
            Authorization: 'Bearer ' + store.getters.StateToken
        };
    }
    return config;
});

let refresh = false;
instance.interceptors.response.use(undefined, async (error: any) => {
    if (error.response.status === 401 && error.config && !refresh) {
        refresh = true;
        try {
            await store.dispatch('Refresh');
            return instance.request(error.config);
        } catch {
            await store.dispatch('Logout');
            router.push('/login');
        } finally {
            refresh = false;
        }
    }
    return Promise.reject(error);
});

export default instance;