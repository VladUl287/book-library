import { Guid } from "guid-typescript"

export type AuthState = {
    email: string | undefined,
    access_token: string | undefined
}

export type AuthSuccess = {
    email: string,
    accessToken: string,
    refreshToken: string
}

export type Book = {
    id: Guid,
    name: string,
    description: string,
    bookmark: boolean,
    image: string,
    pagesCount: number,
    authors: Author[]
}

export type Author = {
    id: Guid,
    name: string
}

export type LoginForm = {
    email: string,
    password: string
}

export type RegisterForm = {
    email: string,
    password: string,
    confirmPassword: string
}