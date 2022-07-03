import instance from '@/http';
import axios from 'axios';
import { RegisterForm, LoginForm, AuthSuccess } from '@/common/contracts';

type AuthState = {
    email: string | undefined
    access_token: string | undefined
}

const state: AuthState = {
    email: undefined,
    access_token: undefined
};

const getters = {
    isAuthenticated: (state: AuthState) => !!state.access_token,
    StateUser: (state: AuthState) => state.email,
    StateToken: (state: AuthState) => state.access_token,
};

const actions = {
    async Register(_: any, form: RegisterForm) {
        const user = getFormData(form);
        await instance.post<AuthSuccess>('auth/register', user)
    },
    async Login({ commit }: any, form: LoginForm) {
        const user = getFormData(form);
        const result = await instance.post<AuthSuccess>('auth/login', user);
        await commit('setAuth', result.data);
    },
    async Logout({ commit }: any) {
        await commit('logout')
        await instance.post('auth/logout')
    },
    async Refresh({ commit }: any) {
        const result = await axios.post<AuthSuccess>(instance.defaults.baseURL + 'auth/refresh', {},
            {
                withCredentials: true
            });
        await commit('setAuth', result.data);
    }
};

const mutations = {
    setAuth(state: AuthState, data: AuthSuccess) {
        state.email = data.email
        state.access_token = data.accessToken
    },
    logout(state: AuthState) {
        state.email = undefined
        state.access_token = undefined
    },
};

export default {
    state,
    getters,
    actions,
    mutations
};

const getFormData = (form: object) => {
    const formData = new FormData();
    for (const [key, value] of Object.entries(form)) {
        formData.append(key, value);
    }
    return formData;
}