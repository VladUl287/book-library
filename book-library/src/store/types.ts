import { Book, Genre } from "@/common/contracts"
import { Guid } from "guid-typescript"

export type RootState = {
    books: BookState,
    genres: GenreState,
    bookmarks: BookmarkState,
    auth: AuthState
}

export type BookState = {
    books: Book[]
    filters: BooksFilter
}

export type BooksFilter = {
    name?: string,
    authorId?: Guid,
    genres?: Guid[],
    rating?: number,
    beginYear?: number,
    endYear?: number
}

export type AuthState = {
    email: string | undefined
    access_token: string | undefined
}

export type BookmarkState = {
    bookmarks: Book[]
}

export type GenreState = {
    genres: Genre[]
}