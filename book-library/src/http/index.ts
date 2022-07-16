import { authModule } from './../store/modules/auth';
import axios, { AxiosRequestConfig } from 'axios';
import router from '@/router';

const instance = axios.create({
    baseURL: 'https://localhost:7031/'
});

instance.interceptors.request.use((config: AxiosRequestConfig<any>) => {
    if (authModule.accessToken) {
        config.headers = {
            Authorization: 'Bearer ' + authModule.accessToken
        };
    }
    return config;
});

let refresh = false;
instance.interceptors.response.use(undefined, async (error: any) => {
    if (error.response.status === 401 && error.config && !refresh) {
        refresh = true;
        try {
            await authModule.Refresh();
            return instance.request(error.config);
        } catch {
            try {
                await authModule.Logout();
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