import { Guid } from "guid-typescript"

export type LoginForm = {
    email: string,
    password: string
}

export type RegisterForm = {
    email: string,
    password: string,
    confirmPassword: string
}

export type AuthSuccess = {
    email: string,
    accessToken: string,
    refreshToken: string
}

interface BookBase {
    name: string,
    description: string,
    pagesCount: number,
    authors: Author[],
    genres: Genre[]
}

export interface Book extends BookBase {
    id: Guid,
    bookmark: boolean,
    image: string,
}

export type CreateBook = BookBase & {
    image: File,
}

export type Author = {
    id: Guid,
    name: string
}

export type Genre = {
    id: Guid,
    name: string
}

export type Collection = {
    id: Guid,
    name: string,
    description: string,
    views: number,
    likes: number,
    books: Book[]
}

export type CollectionManageBook = {
    collectionId: Guid,
    bookId: Guid
}

export type Review = {
    id: Guid,
    text: string,
    rating: number,
    bookId: Guid
}