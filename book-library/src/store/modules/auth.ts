import { AuthActions, AuthMutations } from '../common/enums';
import { ActionTree, GetterTree, MutationTree, Module, ActionContext as AC } from 'vuex';
import instance from '@/http';
import axios from 'axios';
import { RegisterForm, LoginForm, AuthSuccess } from '@/common/contracts';
import { getFormData } from '../common/helpers';
import { AuthState, RootState } from '../common/types';

const state: AuthState = {
    email: undefined,
    access_token: undefined
}

const getters: GetterTree<AuthState, RootState> = {
    isAuthenticated: (state: AuthState) => !!state.access_token,
    getAccessToken: (state: AuthState) => state.access_token,
    getUser: (state: AuthState) => state.email
}

const actions: ActionTree<AuthState, RootState> = {
    async [AuthActions.REGISTER](_: AC<AuthState, RootState>, form: RegisterForm): Promise<void> {
        const user = getFormData(form);
        await instance.post<AuthSuccess>('auth/register', user)
    },
    async [AuthActions.LOGIN]({ commit }: AC<AuthState, RootState>, form: LoginForm): Promise<void> {
        const user = getFormData(form);
        const result = await instance.post<AuthSuccess>('auth/login', user);
        commit(AuthMutations.SET_AUTH, result.data);
    },
    async [AuthActions.LOGOUT]({ commit }: AC<AuthState, RootState>): Promise<void> {
        commit(AuthMutations.LOGOUT)
        await instance.post('auth/logout')
    },
    async [AuthActions.REFRESH]({ commit }: AC<AuthState, RootState>): Promise<void> {
        const result = await axios.post<AuthSuccess>(instance.defaults.baseURL + 'auth/refresh', {},
            {
                withCredentials: true
            });
        commit(AuthMutations.SET_AUTH, result.data);
    }
}

const mutations: MutationTree<AuthState> = {
    [AuthMutations.SET_AUTH](state: AuthState, data: AuthSuccess): void {
        state.email = data.email
        state.access_token = data.accessToken
    },
    [AuthMutations.LOGOUT](state: AuthState): void {
        state.email = state.access_token = undefined
    },
}

export const auth: Module<AuthState, RootState> = {
    state,
    getters,
    actions,
    mutations
}