import axios from "axios";
import { store } from "..";
import instance from "@/http";
import { getFormData } from "../common/helpers";
import { AuthSuccess, LoginForm, RegisterForm } from "@/common/contracts";
import { Module, VuexModule, Mutation, Action, getModule } from "vuex-module-decorators";

@Module({ dynamic: true, store: store, name: 'authModule', preserveState: localStorage.getItem('vuex') !== null })
class AuthModule extends VuexModule {
    private _email?: string;
    private _access_token?: string;

    get email(): string | undefined {
        return this._email
    }

    get accessToken(): string | undefined {
        return this._access_token
    }

    get isAuthenticated(): boolean {
        return !!this._access_token;
    }

    @Mutation
    setAuth(auth: AuthSuccess): void {
        this._email = auth.email
        this._access_token = auth.accessToken
    }

    @Mutation
    logout(): void {
        this._email = this._access_token = undefined
    }

    @Action
    async Register(form: RegisterForm): Promise<void> {
        const user = getFormData(form);
        await instance.post<AuthSuccess>('auth/register', user)
    }

    @Action
    async Login(form: LoginForm): Promise<void> {
        const user = getFormData(form);
        const result = await instance.post<AuthSuccess>('auth/login', user)
        this.setAuth(result.data);
    }

    @Action
    async Logout(): Promise<void> {
        await instance.post('auth/logout')
        this.logout();
    }

    async Refresh(): Promise<void> {
        const result = await axios.post<AuthSuccess>(instance.defaults.baseURL + 'auth/refresh', {},
        {
            withCredentials: true
        });
        this.setAuth(result.data);
    }
}

export const authModule = getModule(AuthModule, store)