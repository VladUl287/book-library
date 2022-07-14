import { AuthActions, AuthMutations } from './../enums';
import { ActionTree, GetterTree, MutationTree, Module } from 'vuex';
import instance from '@/http';
import axios from 'axios';
import { RegisterForm, LoginForm, AuthSuccess, AuthState } from '@/common/contracts';
import { getFormData } from '../helpers';
import { RootState } from '../types';

const state: AuthState = {
    email: undefined,
    access_token: undefined
}

const getters: GetterTree<AuthState, RootState> = {
    isAuthenticated: (state: AuthState) => !!state.access_token,
    getEmail: (state: AuthState) => state.email,
    getToken: (state: AuthState) => state.access_token,
}

const actions: ActionTree<AuthState, RootState> = {
    async [AuthActions.REGISTER](_: any, form: RegisterForm): Promise<void> {
        const user = getFormData(form);
        await instance.post<AuthSuccess>('auth/register', user)
    },
    async [AuthActions.LOGIN]({ commit }: any, form: LoginForm): Promise<void> {
        const user = getFormData(form);
        const result = await instance.post<AuthSuccess>('auth/login', user);
        await commit(AuthMutations.SET_AUTH, result.data);
    },
    async [AuthActions.LOGOUT]({ commit }: any): Promise<void> {
        await commit(AuthMutations.LOGOUT)
        await instance.post('auth/logout')
    },
    async [AuthActions.REFRESH]({ commit }: any): Promise<void> {
        const result = await axios.post<AuthSuccess>(instance.defaults.baseURL + 'auth/refresh', {},
            {
                withCredentials: true
            });
        await commit(AuthMutations.SET_AUTH, result.data);
    }
}

const mutations: MutationTree<AuthState> = {
    [AuthMutations.SET_AUTH](state: AuthState, data: AuthSuccess) {
        state.email = data.email
        state.access_token = data.accessToken
    },
    [AuthMutations.LOGOUT](state: AuthState) {
        state.email = state.access_token = undefined
    },
}

export const auth: Module<AuthState, RootState> = {
    state,
    getters,
    actions,
    mutations
}