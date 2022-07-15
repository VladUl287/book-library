import { Collection, Review } from '../../common/contracts';
import { Book, Genre } from "@/common/contracts"
import { Guid } from "guid-typescript"

export type RootState = {}

export type BookState = {
    books: Book[]
    filters: BooksFilter
}

export type PageFilter = {
    page?: number,
    size?: number,
}

export type BooksFilter = PageFilter & {
    name?: string,
    genres?: Guid[],
    rating?: number,
    beginYear?: number,
    endYear?: number
}

export type CollectionFilter = PageFilter & {
    viewsSort?: boolean,
}

export type ReviewFilter = PageFilter & {
    viewsSort?: boolean,
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

export type CollectionState = {
    collections: Collection[],
    userCollections: Collection[],
    filters: CollectionFilter
}

export type ReviewState = {
    reviews: Review[],
    filters: ReviewFilter
}